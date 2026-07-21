const app = require("../index");

const sendToUnity = async (interactionData) => {
  // console.log(interactionData);
  const unityClient = app?.locals?.unityClient;

  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;
  await unityClient.send(JSON.stringify({ ...interactionData }));

  console.log(`[Sent to Unity]: ${{ ...interactionData }}`);
};

module.exports = sendToUnity;
