const { WebcastEvent } = require("tiktok-live-connector");

const app = require("../index");

const player = require("./player");

const tiktokConnection = require("../connections/tiktok");

const constants = require("../untils/constants");

const interact = async (viewerData) => {
  if (tiktokConnection._connectState !== "CONNECTED") return;

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

  player.enqueue({ ...playerData });
};

tiktokConnection.on(WebcastEvent.CHAT, interact);
tiktokConnection.on(WebcastEvent.GIFT, interact);
