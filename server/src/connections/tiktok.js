const { TikTokLiveConnection } = require("tiktok-live-connector");
const readline = require("readline");

const app = require("../index");

const constants = require("../untils/constants");
const convertToBuffer = require("../untils/convert_to_buffer");

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

  if (key.name === "b") {
    console.log("send base to unity!");
    app.locals.unityClient.send(
      convertToBuffer({
        ...constants.playerConfig[0],
        displayId: "initialPlayer",
        borderColor: "red",
        avatarBuffer: Buffer.from(constants.test.avatarBuffer, "hex"),
      }),
    );
  } else if (key.name === "r") {
    console.log("send rare to unity!");
    app.locals.unityClient.send(
      convertToBuffer({
        ...constants.playerConfig[1],
        displayId: "initialPlayer",
        borderColor: "green",
        avatarBuffer: Buffer.from(constants.test.avatarBuffer, "hex"),
      }),
    );
  } else if (key.name === "m") {
    console.log("send mythic to unity!");
    app.locals.unityClient.send(
      convertToBuffer({
        ...constants.playerConfig[2],
        displayId: "initialPlayer",
        borderColor: "black",
        avatarBuffer: Buffer.from(constants.test.avatarBuffer, "hex"),
      }),
    );
  } else if (key.name === "l") {
    console.log("send legendary to unity!");
    app.locals.unityClient.send(
      convertToBuffer({
        ...constants.playerConfig[3],
        displayId: "initialPlayer",
        borderColor: "orange",
        avatarBuffer: Buffer.from(constants.test.avatarBuffer, "hex"),
      }),
    );
  } else if (key.name === "g") {
    console.log("send god to unity!");
    app.locals.unityClient.send(
      convertToBuffer({
        ...constants.playerConfig[4],
        displayId: "initialPlayer",
        borderColor: "white",
        avatarBuffer: Buffer.from(constants.test.avatarBuffer, "hex"),
      }),
    );
  }
});

module.exports = tiktokConnection;
