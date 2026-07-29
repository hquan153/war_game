const app = require("../index");

const fetchAvatarAsBuffer = require("./fetch_avatar");
const convertToBuffer = require("../untils/convert_to_buffer");

const borderColors = ["green", "red", "black", "yellow", "orange"];
const playerQueue = [];
const displayIds = [];

const spawnInterval = 400; // miliseconds

const player = {
  init() {
    setInterval(() => {
      if (playerQueue.length === 0) return;
      // const unityClient = app.locals?.unityClient;
      // if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

      this.dequeue();
    }, spawnInterval);
  },

  isPlayerQueuing(displayId) {
    return playerQueue.map((player) => player.displayId).includes(displayId);
  },

  enqueue(playerData) {
    playerQueue.push(playerData);
  },

  async dequeue() {
    console.log(playerQueue.length);

    const playerData = playerQueue.shift();

    const { isSendToUnity, attended } = this.attended(playerData.displayId, playerData.diamondCount);
    console.log(playerData.displayId, playerData.diamondCount, isSendToUnity, attended);
    if (!isSendToUnity) return;

    if (playerData.diamondCount > 0) {
      // console.log(playerData);
    }

    playerData.attended = attended;
    playerData.borderColor = borderColors[Math.floor(Math.random() * borderColors.length)];
    playerData.avatarBuffer = await fetchAvatarAsBuffer(playerData.avatarUrl);

    // app.locals.unityClient.send(convertToBuffer(playerData), { binary: true });
  },

  attended(displayId, diamondCount) {
    let isSendToUnity = true;
    for (const displayIdAttend of displayIds) {
      if (diamondCount !== 0) break;
      else if (displayIdAttend === displayId) {
        isSendToUnity = false;
        break;
      }
    }

    if (!displayIds.includes(displayId)) {
      displayIds.push(displayId);
      return { isSendToUnity, attended: false };
    }

    return { isSendToUnity, attended: true };
  },

  remove(displayId, isAll = false) {
    if (isAll) {
      displayIds.length = 0;
      return;
    }

    displayIds.splice(displayIds.indexOf(displayId));
  },
};

player.init();

module.exports = player;
