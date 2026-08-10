const app = require("../index");

const avatarGetter = require("./avatar_getter");

const convertToBuffer = require("../untils/convert_to_buffer");
const constants = require("../untils/constants");

const borderColors = ["green", "red", "black", "yellow", "orange"];
const playerQueue = [];
const displayIds = [];

const checkInterval = 25;
const spawnInterval = 225;

const minPlayer = 4;
let playerCount = 0;

const player = {
  initialPlayersData: [
    {
      ...constants.playerConfig[0],
      count: 2,
    },
    {
      ...constants.playerConfig[1],
      count: 2,
    },
    {
      ...constants.playerConfig[2],
      count: 1,
    },
    {
      ...constants.playerConfig[3],
      count: 0,
    },
    {
      ...constants.playerConfig[4],
      count: 0,
    },
  ],

  init(newPlayersData = null) {
    const playersData = newPlayersData ?? this.initialPlayersData;
    for (const playerData of playersData) {
      playerData.isInit = true;
      playerData.displayId = "initialPlayer";
      playerData.count = playerData.count ?? 1;
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
      playerCount++;
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

    playerCount--;
  },

  playerCountController() {
    // console.log(playerCount);
    if (playerCount >= minPlayer) return;
    const playerData = constants.playerConfig[Math.floor(Math.random() * 2)];
    this.init([{ ...playerData }]);
  },

  async sleep(ms) {
    await new Promise((res) => setTimeout(res, ms));
  },
};

(async () => {
  while (true) {
    await player.sleep(checkInterval);
    try {
      const unityClient = app.locals?.unityClient;
      if (unityClient?.readyState !== WebSocket.OPEN) continue;
      player.playerCountController();
      if (playerQueue.length === 0) continue;
      await player.dequeue();
      await player.sleep(spawnInterval);
    } catch (error) {
      console.error(`ERROR IN IIFE!: ${error}`);
    }
  }
})();

module.exports = { ...player };
