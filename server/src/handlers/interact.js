const { WebcastEvent } = require("tiktok-live-connector");

const app = require("../index");

const tiktokConnection = require("../connections/tiktok");

const { player, playerConfig } = require("./player");
const fetchAvatarAsBuffer = require("./fetch_avatar");
const sendToUnity = require("./send_to_unity");

const constants = require("../untils/constants");
// console.log(playerConfig);

const borderColors = ["green", "red", "black", "yellow", "orange"];

const interact = async (viewerData) => {
  // const unityClient = app.locals?.unityClient;
  // if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  viewerData.diamondCount = viewerData.gift?.diamondCount || 0;
  const playerData =
    playerConfig.find((tier) => tier.diamondCount === viewerData.diamondCount) || playerConfig[0];

  player.enqueue(playerData);

  const { isSendToUnity, attended } = player.attended(
    viewerData.user.displayId,
    viewerData.diamondCount,
  );

  // if (!isSendToUnity) return;

  // console.log("test");

  sendToUnity({ ...playerData });
};

tiktokConnection.on(WebcastEvent.CHAT, interact);
tiktokConnection.on(WebcastEvent.GIFT, interact);

module.exports = interact;
