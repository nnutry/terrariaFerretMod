using Microsoft.Xna.Framework;
using FerretMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FerretMod.Content.Items.Ammo;

namespace FerretMod.Content.Items.Weapons
{
    public class FerretGun : ModItem
    {
        public override void SetDefaults()
        {            
            // Visual properties
            Item.width = 70;
            Item.height = 30;
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;


            // Combat properties
            Item.damage = 0; // Gun damage + bullet damage = final damage
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 20; // Delay between shots.
            Item.useAnimation = 20; // How long shoot animation lasts in ticks.
            Item.knockBack = 1f; // Gun knockback + bullet knockback = final knockback
            Item.autoReuse = true;

            // Other properties
            Item.value = 100000;
            Item.UseSound = SoundID.Item85;

            // Gun properties
            Item.noMelee = true;
            Item.shoot = ProjectileID.PurificationPowder; // does not mean anything here because it is replaced by ammo
            Item.shootSpeed = 1f; // Speed of a projectile
            Item.useAmmo = ModContent.ItemType<FerretSpit>(); // What ammo gun uses
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<FerretEye>(4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override Vector2? HoldoutOffset() => new Vector2(-8f, -4f); // Offset in pixels at which the player will hold the gun. -Y is up
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Умножение на 40f смещает точку появления снарядка вперед на 40 пикселей.
            position += Vector2.Normalize(velocity) * 40f;
        }

    }
}