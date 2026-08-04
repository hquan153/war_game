const axios = require("axios");
const sharp = require("sharp");

const circleShape = Buffer.from(
  '<svg width="72" height="72"><circle cx="36" cy="36" r="36" fill="rgba(255, 255, 255, 1)"/></svg>',
);

const fetchAvatarAsBuffer = async (avatarUrl) => {
  try {
    const response = await axios.get(avatarUrl, { responseType: "arraybuffer" });
    return await sharp(response.data)
      .resize(72, 72)
      .composite([{ input: circleShape, blend: "dest-in" }])
      .png()
      .toBuffer();
  } catch (error) {
    console.error("Error when fetch/convert avatar:", error);
    return null;
  }
};

/* const fs = require("fs");
(async () => {
  const buffer = await fetchAvatarAsBuffer();
  fs.writeFileSync("./avatarBufferTest.txt", buffer.toString("hex"), { encoding: "utf8", flag: "w" });
  console.log("Avatar buffer saved to avatarBufferTest.txt");
})(); */

module.exports = fetchAvatarAsBuffer;
