const { TikTokLiveConnection } = require("tiktok-live-connector");
const readline = require("readline");

const constants = require("../untils/constants");

const tiktokUsername = constants.tiktokUsername;
const tiktokConnection = new TikTokLiveConnection(tiktokUsername, {});

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
  if (key.name === "rare") {
    tiktokConnection.emit("gift", {
      color: "blue",
      size: 1,
      health: 50,
      damage: 2,
      diamonds: 1,
      tier: "rare",
      avatarBase64: constants.avatarBase64Test,
    });
  }
});

module.exports = tiktokConnection;
