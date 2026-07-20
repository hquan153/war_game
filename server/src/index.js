const express = require("express");

const app = express();
app.locals.unityClient = null;
module.exports = app;

require("dotenv").config();

require("./connections/tiktok");
require("./connections/wss");

require("./handlers/receive_gift");

const constants = require("./untils/constants");

/* tiktokConnection.on(WebcastEvent.CHAT, (data) => {
  console.log(`${data.user.uniqueId} writes: ${data.comment}`);
  }); */

app.listen(constants.port, () => {
  console.log(`[HTTP]: ${constants.port}`);
});
