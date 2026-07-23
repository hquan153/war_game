class Gift {
  constructor(color, borderColor, tier, size, health, damage, diamondCount, avatarBuffer) {
    this.color = color;
    this.borderColor = borderColor || "black";
    this.tier = tier;
    this.size = size;
    this.health = health;
    this.damage = damage;
    this.diamondCount = diamondCount;
    this.avatarBuffer = avatarBuffer || "";

    this.isWelcome = false;
    this.message = "";
  }
}

/**
 * 0 (comment) - base
 * 1 - rare
 * 5 - mythic
 * 10 - legendary
 * 100 - god
 */

// color, bordeColor, tier, size, health, damage, diamondCount, avatarBuffer

const giftConfig = [
  new Gift("white", null, "base", 0.4, 20, 0, 0),
  new Gift("blue", null, "rare", 1, 50, 2, 1),
  new Gift("violet", null, "mythic", 2, 100, 2, 5),
  new Gift("yellow", null, "legendary", 3.5, 500, 15, 10),
  new Gift("red", null, "god", 6, 10000, 50, 100),
];

module.exports = giftConfig;
