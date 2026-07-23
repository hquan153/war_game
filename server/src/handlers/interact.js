const { WebcastEvent } = require("tiktok-live-connector");

const tiktokConnection = require("../connections/tiktok");

const fetchAvatarAsBuffer = require("./fetch_avatar");
const sendToUnity = require("./send_to_unity");

const constants = require("../untils/constants");
const giftConfig = require("../untils/gift_config");
// console.log(giftConfig);

const borderColors = ["green", "pink", "red", "black", "yellow", "orange", "purple"];

const interact = async (viewerData) => {
  viewerData.diamondCount = viewerData.gift?.diamondCount || 0;
  // console.log("coin: ", viewerData.diamondCount);

  const interactionData =
    giftConfig.find((gift) => gift.diamondCount === viewerData.diamondCount) || giftConfig[0];

  interactionData.avatarBuffer = await fetchAvatarAsBuffer(viewerData.user.avatarThumb.urlList);
  interactionData.borderColor = borderColors[Math.floor(Math.random() * borderColors.length)];

  // console.log("have an interaction!");
  sendToUnity({ ...interactionData });
};

tiktokConnection.on(WebcastEvent.CHAT, interact);
tiktokConnection.on(WebcastEvent.GIFT, interact);
