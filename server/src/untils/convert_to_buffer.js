const convertToBuffer = ({ avatarBuffer, ...playerData }) => {
  const playerDataBuffer = Buffer.from(JSON.stringify(playerData), "utf8");

  const lengthHeader = Buffer.alloc(4);
  lengthHeader.writeInt32LE(playerDataBuffer.length);

  return Buffer.concat([lengthHeader, playerDataBuffer, avatarBuffer || Buffer.alloc(0)]);
};

module.exports = convertToBuffer;
