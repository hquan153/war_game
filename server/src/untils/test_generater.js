const fs = require("fs");

const avatarGetter = require("../handlers/avatar_getter");

const avatarUrlTest =
  "https://p16-common-sign.tiktokcdn.com/tos-alisg-avt-0068/938fe119f397e15c67dea9884b41a71a~tplv-tiktok-shrink:72:72.webp?dr=14561&refresh_token=9532ccbe&x-expires=1786460400&x-signature=UCzq67e5aROrbDCRLz5wJ5ThZe8%3D&t=4d5b0474&ps=13740610&shp=a5d48078&shcp=fdd36af4&idc=my2";

(async () => {
  const buffer = await avatarGetter(avatarUrlTest);
  fs.writeFileSync("../assets/avatarBufferTest.png", buffer);
  console.log("Avatar saved to avatarBufferTest.png");
})();
