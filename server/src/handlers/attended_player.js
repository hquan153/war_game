const displayIds = [];

const attendedPlayer = (displayId, diamondCount) => {
  console.log(displayIds, displayId);

  let isSendToUnity = true;
  for (const displayIdAttend of displayIds) {
    if (diamondCount !== 0) break;
    else if (displayIdAttend === displayId) {
      isSendToUnity = false;
      break;
    }
  }

  if (!displayIds.includes(displayId)) {
    displayIds.push(displayId);
    return { isSendToUnity, attended: false };
  }

  return { isSendToUnity, attended: true };
};

module.exports = attendedPlayer;
