class Gift {
  constructor(borderColor, health, damage, diamondCount, tier, avatarBase64) {
    this.borderColor = borderColor;
    this.health = health;
    this.damage = damage;
    this.diamondCount = diamondCount;
    this.tier = tier;
    this.avatarBase64 = avatarBase64 || "";
  }
}

/**
 * 0 (comment) - base
 * 1 - rare
 * 5 - mythic
 * 10 - legendary
 * 100 - god
 */

// border-color, health, size, damage, diamondCount, tier, avatarBase64

const giftConfig = [
  new Gift("white", 20, 0, 0, "base"),
  new Gift("blue", 50, 2, 1, "rare"),
  new Gift("violet", 100, 2, 5, "mythic"),
  new Gift("yellow", 500, 15, 10, "legendary"),
  new Gift("red", 10000, 50, 100, "god"),
];

module.exports = giftConfig;

/* [
  {
    avatar: '',
    borderColor: 'white',
    health: 20,
    size: 1,
    damage: 0,
    diamonds: 0,
    tier: 'base'
  },
  {
    avatar: '',
    borderColor: 'blue',
    health: 50,
    size: 2,
    damage: 2,
    diamonds: 1,
    tier: 'rare'
  },
  {
    avatar: '',
    borderColor: 'violet',
    health: 100,
    size: 5,
    damage: 2,
    diamonds: 5,
    tier: 'mythic'
  },
  {
    avatar: '',
    borderColor: 'yellow',
    health: 500,
    size: 10,
    damage: 15,
    diamonds: 10,
    tier: 'legendary'
  },
  {
    avatar: '',
    borderColor: 'red',
    health: 10000,
    size: 25,
    damage: 50,
    diamonds: 100,
    tier: 'god'
  }
] */
