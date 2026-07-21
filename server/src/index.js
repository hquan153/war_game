const express = require("express");

const app = express();
app.locals.unityClient = null;
module.exports = app;

require("dotenv").config();

require("./connections/tiktok");
require("./connections/wss");

require("./handlers/interact");

const constants = require("./untils/constants");

app.listen(constants.port, () => {
  console.log(`[HTTP]: ${constants.port}`);
});
