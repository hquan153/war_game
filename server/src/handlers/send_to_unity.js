const app = require("../index");

const convertToBuffer = require("../untils/convert_to_buffer");

let i = 0;
const sendToUnity = (interactionData) => {
  const unityClient = app?.locals?.unityClient;
  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  unityClient.send(convertToBuffer(interactionData), { binary: true });

  console.log(`[Sent to Unity]: ${i}, coin: ${interactionData.diamondCount}`);
  i++;
};

module.exports = sendToUnity;
