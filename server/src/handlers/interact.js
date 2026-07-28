const { WebcastEvent } = require("tiktok-live-connector");

const tiktokConnection = require("../connections/tiktok");

const attendedPlayer = require("./attended_player");
const fetchAvatarAsBuffer = require("./fetch_avatar");
const sendToUnity = require("./send_to_unity");

const constants = require("../untils/constants");
const playerConfig = require("../untils/player_config");
// console.log(playerConfig);

const attendedIds = [];
const borderColors = ["green", "red", "black", "yellow", "orange"];
const interact = async (viewerData) => {
  viewerData.diamondCount = viewerData.gift?.diamondCount || 0;
  const { isSendToUnity, attended } = attendedPlayer(viewerData.user.displayId, viewerData.diamondCount);
  console.error(isSendToUnity);
  if (!isSendToUnity) return;

  // console.log("test");

  const interactionData =
    playerConfig.find((gift) => gift.diamondCount === viewerData.diamondCount) || playerConfig[0];

  interactionData.displayId = viewerData.user.displayId;
  interactionData.attended = attended;
  interactionData.borderColor = borderColors[Math.floor(Math.random() * borderColors.length)];
  interactionData.avatarBuffer = await fetchAvatarAsBuffer(viewerData.user.avatarThumb.urlList);

  sendToUnity({ ...interactionData });
};

tiktokConnection.on(WebcastEvent.CHAT, interact);
tiktokConnection.on(WebcastEvent.GIFT, interact);

module.exports = interact;
