const app = require("../index");

let i = 0;
const sendToUnity = async (interactionData) => {
  // console.log(interactionData);
  const unityClient = app?.locals?.unityClient;

  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;
  await unityClient.send(JSON.stringify({ ...interactionData }));

  console.log(Buffer.isBuffer(interactionData.avatarBase64), interactionData.avatarBase64.length);
  i++;

  console.log(`[Sent to Unity]: ${{ ...interactionData }}`);
};

module.exports = sendToUnity;
