const app = require("../index");

const fetchAvatarAsBuffer = require("./fetch_avatar");
const convertToBuffer = require("../untils/convert_to_buffer");

const borderColors = ["green", "red", "black", "yellow", "orange"];
const playerQueue = [];
const displayIds = [];

const checkInterval = 30;
const spawnInterval = 170;

const player = {
  enqueue(playerData) {
    // console.log(playerData.displayId);
    playerQueue.push(playerData);
  },

  async dequeue() {
    const playerData = { ...playerQueue.shift() };

    const { isSendToUnity, attended } = this.attended({ ...playerData });
    console.log(
      playerQueue.length,
      playerData.displayId,
      playerData.diamondCount,
      playerData.tier,
      isSendToUnity,
      attended,
    );
    if (!isSendToUnity) return;

    playerData.attended = attended;
    playerData.borderColor = borderColors[Math.floor(Math.random() * borderColors.length)];
    playerData.avatarBuffer = await fetchAvatarAsBuffer(playerData.avatarUrl);

    // console.log(`queue: ${playerQueue.map((player) => player.displayId)}`);
    // console.log(`attended: ${displayIds}`);

    await app.locals.unityClient.send(convertToBuffer(playerData), { binary: true });
  },

  attended({ displayId, diamondCount }) {
    const m_attended = displayIds.includes(displayId);
    const isSendToUnity = diamondCount > 0 || !m_attended;

    !m_attended && displayIds.push(displayId);

    return { isSendToUnity, attended: m_attended };
  },

  remove(displayId, isAll = false) {
    if (isAll) {
      displayIds.length = 0;
      return;
    }

    displayIds.splice(displayIds.indexOf(displayId));
  },

  async sleep(ms) {
    await new Promise((res) => setTimeout(res, ms));
  },
};

(async () => {
  while (true) {
    const unityClient = app.locals?.unityClient;
    if (playerQueue.length > 0 && unityClient?.readyState === WebSocket.OPEN) {
      await player.dequeue();
      await player.sleep(spawnInterval);
    }
    await player.sleep(checkInterval);
  }
})();

module.exports = { ...player };
