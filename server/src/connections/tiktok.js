const { TikTokLiveConnection } = require("tiktok-live-connector");
const fs = require("fs");
const readline = require("readline");

const playerConfig = require("../untils/player_config");

const constants = require("../untils/constants");

const tiktokUsername = constants.tiktokUsername;
const tiktokConnection = new TikTokLiveConnection(tiktokUsername, {});

const avatarBufferTest = fs.readFileSync("./avatarBufferTest.txt", {
  encoding: "utf8",
  flag: "r",
});

/* tiktokConnection
  .connect()
  .then((state) => {
    console.info(`Connected to roomId ${state.roomId}`);
  })
  .catch((err) => {
    console.error("Failed to connect", err);
  }); */

const sendToUnity = require("../handlers/send_to_unity");

readline.emitKeypressEvents(process.stdin);
if (process.stdin.isRawMode) process.stdin.setRawMode(true);

process.stdin.on("keypress", (str, key) => {
  if (key.name === "b") {
    console.log("send rare to unity!");
    sendToUnity({
      ...playerConfig[0],
      displayId: "sangotinh09",
      borderColor: "red",
      avatarBuffer: Buffer.from(avatarBufferTest, "hex"),
    });
  } else if (key.name === "r") {
    console.log("send rare to unity!");
    sendToUnity({
      ...playerConfig[1],
      displayId: "sangotinh09",
      borderColor: "green",
      avatarBuffer: Buffer.from(avatarBufferTest, "hex"),
    });
  } else if (key.name === "p") {
    console.log("send rare to unity!");
    sendToUnity({
      ...playerConfig[2],
      displayId: "sangotinh09",
      borderColor: "black",
      avatarBuffer: Buffer.from(avatarBufferTest, "hex"),
    });
  } else if (key.name === "y") {
    console.log("send rare to unity!");
    sendToUnity({
      ...playerConfig[3],
      displayId: "sangotinh09",
      borderColor: "orange",
      avatarBuffer: Buffer.from(avatarBufferTest, "hex"),
    });
  } else if (key.name === "g") {
    sendToUnity({
      ...playerConfig[4],
      displayId: "sangotinh09",
      borderColor: "white",
      avatarBuffer: Buffer.from(avatarBufferTest, "hex"),
    });
  }
});

module.exports = tiktokConnection;
