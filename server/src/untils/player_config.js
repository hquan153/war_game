class Player {
  constructor(
    displayId,
    color,
    borderColor,
    tier,
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
    this.size = size;
    this.health = health;
    this.damage = damage;
    this.diamondCount = diamondCount;
    this.avatarBuffer = avatarBuffer || "";
    this.attended = attended || false;

    this.isWelcome = false;
    this.message = "";
  }
}

const playerConfig = [
  new Player(null, "white", null, "base", 0.4, 20, 0, 0, false, null),
  new Player(null, "blue", null, "rare", 1, 50, 2, 1, false, null),
  new Player(null, "purple", null, "mythic", 2, 100, 2, 5, false, null),
  new Player(null, "yellow", null, "legendary", 3.5, 500, 15, 10, false, null),
  new Player(null, "red", null, "god", 6, 10000, 50, 200, false, null),
];

module.exports = playerConfig;

/**
 * 0 (comment) - base
 * 1 - rare
 * 5 - mythic
 * 10 - legendary
 * 100 - god
 */

// displayId, color, bordeColor, tier, size, health, damage, diamondCount, avatarBuffer
