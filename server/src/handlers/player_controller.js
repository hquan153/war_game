const displayIds = [];

const attendedPlayer = (displayId, diamondCount) => {
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

const removePlayer = (displayId, isAll = false) => {
  if (isAll) {
    displayIds.length = 0;
    return;
  }

  displayIds.splice(displayIds.indexOf(displayId));
};

module.exports = { attendedPlayer, removePlayer };
