const { WebcastEvent } = require("tiktok-live-connector");

const app = require("../index");

const player = require("./player");

const tiktokConnection = require("../connections/tiktok");

const constants = require("../untils/constants");

const interact = async (viewerData) => {
  // console.log(viewerData.gift?.diamondCount);

  // const unityClient = app.locals?.unityClient;
  // if (!unityClient || unityClient.readyState !== WebSocket.OPEN) return;

  if (player.isPlayerQueuing(viewerData.user.displayId) && viewerData.gift?.diamondCount === 0) return;

  const playerData =
    constants.playerConfig.find((tier) => tier.diamondCount === viewerData.gift?.diamondCount || 0) ||
    constants.playerConfig[0];

  playerData.displayId = viewerData.user.displayId;
  playerData.avatarUrl = viewerData.user.avatarThumb.urlList;
  player.enqueue({ ...playerData });
};

tiktokConnection.on(WebcastEvent.CHAT, interact);
tiktokConnection.on(WebcastEvent.GIFT, interact);
