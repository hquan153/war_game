const { WebcastEvent } = require("tiktok-live-connector");

const tiktokConnection = require("../connections/tiktok");

const fetchAvatarAsBase64 = require("./fetch_avatar");
const sendToUnity = require("./send_to_unity");

const constants = require("../untils/constants");
const giftConfig = require("../untils/gift_config");
// console.log(giftConfig);

tiktokConnection.on(WebcastEvent.CHAT, async (viewerData) => {
  /*  // console.log(viewerData);
  console.log(
    `${viewerData.giftDetails.giftName} x${viewerData.repeatCount}, ${viewerData.diamondCount} diamonds,${viewerData.test}`,
  );

  const giftInfo = giftConfig.find((gift) => gift.diamondCount === viewerData.diamondCount);
  giftInfo.avatarBase64 = await fetchAvatarAsBase64(
    viewerData.profilePictureUrl || viewerData.user.profilePicture.urls[0],
  );

  sendToUnity({ ...giftInfo, count: viewerData.test ? 1 : viewerData.repeatCount }); */

  viewerData.diamondCount = 0;
  interact(viewerData);
});

tiktokConnection.on(WebcastEvent.GIFT, async (viewerData) => {
  /* // console.log(viewerData);
  console.log(
    `${viewerData.giftDetails.giftName} x${viewerData.repeatCount}, ${viewerData.diamondCount} diamonds,${viewerData.test}`,
  );

  const giftInfo = giftConfig.find((gift) => gift.diamondCount === viewerData.diamondCount);
  giftInfo.avatarBase64 = await fetchAvatarAsBase64(
    viewerData.profilePictureUrl || viewerData.user.profilePicture.urls[0],
  );

  sendToUnity({ ...giftInfo, count: viewerData.test ? 1 : viewerData.repeatCount }); */

  interact(viewerData);
});

const interact = async (viewerData) => {
  const interactionData =
    giftConfig.find((gift) => gift.diamondCount === viewerData.diamondCount) || giftConfig[0];
  interactionData.avatarBase64 = await fetchAvatarAsBase64(
    viewerData.user.avatarThumb.urlList,
    // viewerData.profilePictureUrl || viewerData.user.profilePicture.urls[0],
  );
  console.log(interactionData);

  sendToUnity({ ...interactionData });
};
