// const fs = require("fs");
const app = require("../index");

const convertToBuffer = require("../untils/convert_to_buffer");

let i = 0;
const sendToUnity = (interactionData) => {
  // console.log(interactionData);

  app.locals.unityClient.send(convertToBuffer(interactionData), { binary: true });

  console.log(`[Sent to Unity]: coin: ${interactionData.diamondCount}, ${i}`);
  i++;
};

module.exports = sendToUnity;
