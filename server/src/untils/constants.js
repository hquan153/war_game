const fs = require("fs");

const constants = {
  port: 3000,
  wsPort: process.env.PORT || 8080,
  username: process.env.TIKTOK_USERNAME || "@" + "mtnguyen15",
  sessionId: process.env.SESSION_ID || "34a5e29039af1d97e7211d9ac2888209",

  test: {
    avatarUrl:
      process.env.AVATAR_URL ||
      "https://p16-common-sign.tiktokcdn.com/tos-alisg-avt-0068/938fe119f397e15c67dea9884b41a71a~tplv-tiktok-shrink:72:72.webp?dr=14561&refresh_token=c0487367&x-expires=1786028400&x-signature=sTk5yP5z3%2BUNjVpvKnbOJjiMs2k%3D&t=4d5b0474&ps=13740610&shp=a5d48078&shcp=fdd36af4&idc=my2",
    avatarBuffer: fs.readFileSync("src/assets/avatarBufferTest.txt", {
      encoding: "utf8",
      flag: "r",
    }),
  },

  playerConfig: [
    {
      displayId: "",
      color: "white",
      borderColor: null,
      tier: "base",
      mass: 1,
      size: 0.4,
      health: 10,
      damage: 1,
      diamondCount: 0,
      attended: null,
      avatarUrl: "",
      avatarBuffer: "",
    },
    {
      displayId: "",
      color: "blue",
      borderColor: null,
      tier: "rare",
      mass: 3,
      size: 0.8,
      health: 100,
      damage: 4,
      diamondCount: 1,
      attended: null,
      avatarUrl: "",
      avatarBuffer: "",
    },
    {
      displayId: "",
      color: "purple",
      borderColor: null,
      tier: "mythic",
      mass: 8,
      size: 2,
      health: 400,
      damage: 10,
      diamondCount: 5,
      attended: null,
      avatarUrl: "",
      avatarBuffer: "",
    },
    {
      displayId: "",
      color: "yellow",
      borderColor: null,
      tier: "legendary",
      mass: 25,
      size: 3.5,
      health: 1500,
      damage: 40,
      diamondCount: 10,
      attended: null,
      avatarUrl: "",
      avatarBuffer: "",
    },
    {
      displayId: "",
      color: "red",
      borderColor: null,
      tier: "god",
      mass: 100,
      size: 5.5,
      health: 10000,
      damage: 150,
      diamondCount: 100,
      attended: null,
      avatarUrl: "",
      avatarBuffer: "",
    },
  ],
};

module.exports = { ...constants };
