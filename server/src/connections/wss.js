const WebSocket = require("ws");

const app = require("../index");

const convertToBuffer = require("../untils/convert_to_buffer");
const constants = require("../untils/constants");

const wss = new WebSocket.Server({ port: constants.wsPort }, () => {
  console.log(`[WS]: ${constants.wsPort}`);
});

wss.on("connection", (ws) => {
  console.log(`[WS]: Unity connected via WebSocket.`);
  app.locals.unityClient = ws;

  ws.send(convertToBuffer({ message: "Welcome to the WebSocket server!", isWelcome: true }));

  ws.on("close", () => {
    console.log(`[WS]: Unity disconnected from WebSocket.`);
    app.locals.unityClient = null;
  });
});
