const { TikTokLiveConnection } = require("tiktok-live-connector");
const readline = require("readline");

const app = require("../index");
const player = require("../handlers/player");
const constants = require("../untils/constants");

const tiktokConnection = new TikTokLiveConnection(constants.username, {
  processInitialData: false,
  sessionId: constants.sessionId,
});

tiktokConnection
  .connect()
  .then((state) => {
    console.info(`[TIKTOK]: Connected to ${constants.username}: roomId ${state.roomId}`);
  })
  .catch((error) => {
    console.error(`[TIKTOK]: Failed to connect ${constants.username}!: ${error}`);
  });

readline.emitKeypressEvents(process.stdin);
if (process.stdin.isRawMode) process.stdin.setRawMode(true);

process.stdin.on("keypress", (str, key) => {
  const unityClient = app.locals.unityClient;
  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  const indexOf = constants.playerTierKey.indexOf(key.name);
  if (indexOf < 0) return;
  player.init([{ ...constants.playerConfig[indexOf] }]);
  console.log(`[KEYPRESS]: Sent ${constants.playerConfig[indexOf].tier} to unity!`);
});

module.exports = tiktokConnection;
