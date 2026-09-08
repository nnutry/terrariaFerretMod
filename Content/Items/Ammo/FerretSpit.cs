using Microsoft.Xna.Framework;
using FerretMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FerretMod.Content.Projectiles;

namespace FerretMod.Content.Items.Ammo
{
    // ITEM
    public class FerretSpit : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.width = 14;
            Item.height = 14;

            // Combat properties
            Item.damage = 5; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0f;
            Item.shoot = ModContent.ProjectileType<Projectiles.FerretSpit>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 3f; // The speed of the projectile. This value equivalent to Silver Bullet since ExampleBullet's Projectile.extraUpdates is 1.

            // Other properties
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.ammo = Item.type; // The ammo class this ammo belongs to. Уникальный тип (слюна)
        }
    }
}
