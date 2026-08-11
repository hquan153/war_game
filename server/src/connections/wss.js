const WebSocket = require("ws");

const app = require("../index");

const player = require("../handlers/player");
const constants = require("../untils/constants");

const wss = new WebSocket.Server({ port: constants.wsPort }, () => {
  console.log(`[WS]: ${constants.wsPort}`);
});

wss.on("connection", (ws) => {
  console.log(`[WS]: Unity connected via WebSocket!`);
  app.locals.unityClient = ws;
  player.init();

  ws.on("message", (message) => {
    // console.log(`[WS] received message: ${message}`);
    player.remove(message);
  });

  ws.on("close", () => {
    console.log(`[WS]: Unity disconnected from WebSocket!`);
    player.remove(null, true);
    app.locals.unityClient = null;
  });
});
