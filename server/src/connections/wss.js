const WebSocket = require("ws");

const app = require("../index");

const player = require("../handlers/player");

const constants = require("../untils/constants");

const websocket = new WebSocket.Server({ port: constants.wsPort }, () => {
  console.log(`[WS]: ${constants.wsPort}`);
});

websocket.on("connection", (ws) => {
  console.log(`[WS]: Unity connected via WebSocket.`);
  app.locals.unityClient = ws;
  player.init();

  ws.on("message", (displayId) => {
    // console.log(`[WS] received displayId: ${displayId}`);
    player.remove(displayId);
  });

  ws.on("close", () => {
    console.log(`[WS]: Unity disconnected from WebSocket.`);
    player.remove(null, true);
    app.locals.unityClient = null;
  });
});
