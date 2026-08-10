const fs = require("fs");

const avatarGetter = require("../handlers/avatar_getter");

(async () => {
  const buffer = await avatarGetter(process.env.AVATAR_URL_TEST);
  fs.writeFileSync("../assets/avatarBufferTest.png", buffer);
  console.log("Avatar saved to avatarBufferTest.png");
})();
