const { TikTokLiveConnection } = require("tiktok-live-connector");
const readline = require("readline");

const app = require("../index");
const player = require("../handlers/player");
const constants = require("../untils/constants");

const tiktokConnection = new TikTokLiveConnection(constants.username, {
  processInitialData: false,
  // sessionId: constants.sessionId,
});

tiktokConnection
  .connect()
  .then((state) => {
    console.info(`Connected to roomId ${state.roomId}`);
  })
  .catch((err) => {
    console.error("Failed to connect", err);
  });

readline.emitKeypressEvents(process.stdin);
if (process.stdin.isRawMode) process.stdin.setRawMode(true);

process.stdin.on("keypress", (str, key) => {
  const unityClient = app.locals?.unityClient;
  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  const indexOf = constants.playerTierKey.indexOf(key.name);
  indexOf >= 0 && player.init([{ ...constants.playerConfig[indexOf] }]);
});

module.exports = tiktokConnection;
