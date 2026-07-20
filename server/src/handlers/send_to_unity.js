const app = require("../index");

const sendToUnity = async (giftInfo) => {
  // console.log(giftInfo);
  const unityClient = app?.locals?.unityClient;

  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;
  await unityClient.send(JSON.stringify({ ...giftInfo }));

  console.log(
    `[Sent to Unity]: ${giftInfo.giftName} x${giftInfo.count}, ${giftInfo.diamondCount} diamonds`,
  );
};

module.exports = sendToUnity;
