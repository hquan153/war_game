const app = require("../index");

const avatarGetter = require("./avatar_getter");

const convertToBuffer = require("../untils/convert_to_buffer");
const constants = require("../untils/constants");

const borderColors = ["green", "red", "black", "yellow", "orange"];
const playerQueue = [];
const displayIds = [];

const checkInterval = 30;
const spawnInterval = 200;

const player = {
  init() {
    const initialPlayerData = [
      {
        ...constants.playerConfig[0],
        count: 1,
      },
      {
        ...constants.playerConfig[1],
        count: 5,
      },
      {
        ...constants.playerConfig[2],
        count: 1,
      },
    ];

    for (const playerData of initialPlayerData) {
      playerData.displayId = "initialPlayer";
      playerData.isInit = true;
      playerData.avatarBuffer = constants.test.avatarBuffer;

      for (let i = 0; i < playerData.count; i++) this.enqueue({ ...playerData });
    }
  },

  enqueue(playerData) {
    playerQueue.push(playerData);
  },

  async dequeue() {
    const playerData = { ...playerQueue.shift() };

    if (!playerData.isInit) {
      const isSendToUnity = this.attended({ ...playerData });
      console.log(playerQueue.length, playerData.displayId, playerData.diamondCount, playerData.tier, isSendToUnity);
      if (!isSendToUnity) return;
    }

    playerData.borderColor = borderColors[Math.floor(Math.random() * borderColors.length)];
    playerData.avatarBuffer = playerData.avatarBuffer ?? (await avatarGetter(playerData.avatarUrl));

    try {
      await app.locals.unityClient.send(convertToBuffer(playerData), { binary: true });
    } catch (error) {
      console.error(`Error when send player to unity: ${error}`);
    }
  },

  attended({ displayId, diamondCount }) {
    const m_attended = displayIds.includes(displayId);
    const isSendToUnity = diamondCount > 0 || !m_attended;

    !m_attended && displayIds.push(displayId);

    return isSendToUnity;
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
    try {
      const unityClient = app.locals?.unityClient;
      if (playerQueue.length > 0 && unityClient?.readyState === WebSocket.OPEN) {
        await player.dequeue();
        await player.sleep(spawnInterval);
      }
      await player.sleep(checkInterval);
    } catch (error) {
      console.error(`ERROR IN IIFE!: ${error}`);
    }
  }
})();

module.exports = { ...player };
