const playerQueue = [];
const displayIds = [];

const spawnInterval = 800; // miliseconds



const player = {
  init() {
    setInterval(() => {
      this.dequeue();
    }, spawnInterval);
  },

  enqueue() {},

  dequeue() {
    playerData.displayId = viewerData.user.displayId;
    playerData.attended = attended;
    playerData.borderColor = borderColors[Math.floor(Math.random() * borderColors.length)];
    playerData.avatarBuffer = await fetchAvatarAsBuffer(viewerData.user.avatarThumb.urlList);
  },

  attended(displayId, diamondCount) {
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
  },

  remove(displayId, isAll = false) {
    if (isAll) {
      displayIds.length = 0;
      return;
    }

    displayIds.splice(displayIds.indexOf(displayId));
  },
};

const playerConfig = [
  {
    displayId: "",
    color: "white",
    borderColor: null,
    tier: "base",
    mass: 1,
    size: 0.4,
    health: 20,
    damage: 1,
    diamondCount: 0,
    attended: null,
    avatarBuffer: "",
    isWelcome: false,
    message: "",
  },
  {
    displayId: "",
    color: "blue",
    borderColor: null,
    tier: "rare",
    mass: 1.5,
    size: 1,
    health: 50,
    damage: 2,
    diamondCount: 1,
    attended: null,
    avatarBuffer: "",
    isWelcome: false,
    message: "",
  },
  {
    displayId: "",
    color: "purple",
    borderColor: null,
    tier: "mythic",
    mass: 3,
    size: 2,
    health: 100,
    damage: 2,
    diamondCount: 5,
    attended: null,
    avatarBuffer: "",
    isWelcome: false,
    message: "",
  },
  {
    displayId: "",
    color: "yellow",
    borderColor: null,
    tier: "legendary",
    mass: 10,
    size: 3.5,
    health: 500,
    damage: 15,
    diamondCount: 10,
    attended: null,
    avatarBuffer: "",
    isWelcome: false,
    message: "",
  },
  {
    displayId: "",
    color: "red",
    borderColor: null,
    tier: "god",
    mass: 50,
    size: 6,
    health: 10000,
    damage: 50,
    diamondCount: 200,
    attended: null,
    avatarBuffer: "",
    isWelcome: false,
    message: "",
  },
];

player.init();

module.exports = { player, playerConfig };

/**
 * 0 (comment) - base
 * 1 - rare
 * 5 - mythic
 * 10 - legendary
 * 100 - god
 */

// displayId, color, bordeColor, tier, size, health, damage, diamondCount, avatarBuffer

/* const playerConfig = [
  new Player(null, "white", null, "base", 1, 0.4, 20, 1, 0, false, null),
  new Player(null, "blue", null, "rare", 1.5, 1, 50, 2, 1, false, null),
  new Player(null, "purple", null, "mythic", 3, 2, 100, 2, 5, false, null),
  new Player(null, "yellow", null, "legendary", 10, 3.5, 500, 15, 10, false, null),
  new Player(null, "red", null, "god", 50, 6, 10000, 50, 200, false, null),
]; */

/*  constructor(
    displayId,
    color,
    borderColor,
    tier,
    mass,
    size,
    health,
    damage,
    diamondCount,
    attended,
    avatarBuffer,
  ) {
    this.displayId = displayId || "";
    this.color = color;
    this.borderColor = borderColor || "black";
    this.tier = tier;
    this.mass = mass;
    this.size = size;
    this.health = health;
    this.damage = damage;
    this.diamondCount = diamondCount;
    this.attended = attended || false;
    this.avatarBuffer = avatarBuffer || "";

    this.isWelcome = false;
    this.message = "";
  } */
