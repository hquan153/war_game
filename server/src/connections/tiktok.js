const { TikTokLiveConnection } = require("tiktok-live-connector");
const readline = require("readline");

const constants = require("../untils/constants");

const tiktokUsername = constants.tiktokUsername;
const tiktokConnection = new TikTokLiveConnection(tiktokUsername);

tiktokConnection
  .connect()
  .then((state) => {
    console.info(`Connected to roomId ${state.roomId}`);
  })
  .catch((err) => {
    console.error("Failed to connect", err);
  });

/* readline.emitKeypressEvents(process.stdin);
if (process.stdin.isRawMode) {
  process.stdin.setRawMode(true);
}

process.stdin.on("keypress", (str, key) => {
  if (key.name === "a") {
    tiktokConnection.emit("gift", {
      order: 1,
      giftDetails: { giftName: "GG" },
      attacker: "Ronaldo",
      target: "Messi",
      damage: 0.01,
      diamondCount: 1,
      test: true,
    });
  }
}); */

module.exports = tiktokConnection;
