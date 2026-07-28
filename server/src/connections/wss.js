const WebSocket = require("ws");

const app = require("../index");

const { removePlayer } = require("../handlers/player_controller");

const convertToBuffer = require("../untils/convert_to_buffer");
const constants = require("../untils/constants");

const websocket = new WebSocket.Server({ port: constants.wsPort }, () => {
  console.log(`[WS]: ${constants.wsPort}`);
});

websocket.on("connection", (ws) => {
  console.log(`[WS]: Unity connected via WebSocket.`);
  app.locals.unityClient = ws;

  ws.send(convertToBuffer({ message: "Welcome to the WebSocket server!", isWelcome: true }));

  ws.on("message", (displayId) => {
    console.log(`[WS] received displayId: ${displayId}`);
    removePlayer(displayId);
  });

  ws.on("close", () => {
    console.log(`[WS]: Unity disconnected from WebSocket.`);
    removePlayer(null, true);
    app.locals.unityClient = null;
  });
});
