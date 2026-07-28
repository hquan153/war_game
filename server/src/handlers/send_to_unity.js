const app = require("../index");

const fs = require("fs");

const convertToBuffer = require("../untils/convert_to_buffer");

let i = 0;
const sendToUnity = (interactionData) => {
  // console.log(interactionData);

  const unityClient = app?.locals?.unityClient;
  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  unityClient.send(convertToBuffer(interactionData), { binary: true });

  console.log(`[Sent to Unity]: coin: ${interactionData.diamondCount}, ${i}`);
  i++;
};

module.exports = sendToUnity;
