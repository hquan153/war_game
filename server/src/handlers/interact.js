const { WebcastEvent } = require("tiktok-live-connector");

const app = require("../index");

const player = require("./player1");

const tiktokConnection = require("../connections/tiktok");

const constants = require("../untils/constants");

const interact = async (viewerData) => {
  if (tiktokConnection._connectState !== "CONNECTED") return;

  // console.log(viewerData.user.displayId, viewerData.user.avatarThumb.urlList[0]);

  const unityClient = app.locals?.unityClient;
  if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  const matchedConfig =
    constants.playerConfig.find((tier) => tier.diamondCount === (viewerData.gift?.diamondCount || 0)) ||
    constants.playerConfig[0];

  const playerData = {
    ...matchedConfig,
    displayId: viewerData.user.displayId,
    avatarUrl: viewerData.user.avatarThumb.urlList,
  };

  for (let i = 0; i < (viewerData.repeatCount ?? 1); i++) player.enqueue({ ...playerData });
};

tiktokConnection.on(WebcastEvent.CHAT, interact);
tiktokConnection.on(WebcastEvent.GIFT, interact);
