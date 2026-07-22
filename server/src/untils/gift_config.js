class Gift {
  constructor(borderColor, tier, size, health, damage, diamondCount, avatarBase64) {
    this.borderColor = borderColor;
    this.tier = tier;
    this.size = size;
    this.health = health;
    this.damage = damage;
    this.diamondCount = diamondCount;
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

// border-color, health, damage, diamondCount, tier, avatarBase64

const giftConfig = [
  new Gift("white", "base", 0.4, 20, 0, 0),
  new Gift("blue", "rare", 1, 50, 2, 1),
  new Gift("violet", "mythic", 2, 100, 2, 5),
  new Gift("yellow", "legendary", 3.5, 500, 15, 10),
  new Gift("red", "god", 6, 10000, 50, 100),
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
