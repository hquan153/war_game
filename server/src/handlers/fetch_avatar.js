const axios = require("axios");
const sharp = require("sharp");

const circleShape = Buffer.from(
  '<svg width="72" height="72"><circle cx="36" cy="36" r="36" fill="rgb(255, 255, 255)"/></svg>',
);

const fetchAvatarAsBase64 = async (avatarUrl) => {
  try {
    const response = await axios.get(avatarUrl, { responseType: "arraybuffer" });
    const roundedPngBuffer = await sharp(response.data)
      .resize(72, 72)
      .composite([
        {
          input: circleShape,
          blend: "dest-in",
        },
      ])
      .toFormat("png")
      .toBuffer();

    return roundedPngBuffer.toString("base64");
  } catch (error) {
    console.error("Lỗi fetch/convert avatar:", error);
    return null;
  }
};

module.exports = fetchAvatarAsBase64;
