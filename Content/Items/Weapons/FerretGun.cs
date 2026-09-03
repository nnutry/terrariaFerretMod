using Microsoft.Xna.Framework;
using FerretMod.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
            Item.useStyle = ItemUseStyleID.Shoot; // Use style for guns
            Item.rare = ItemRarityID.Blue;


            // Combat properties
            Item.damage = 0; // Gun damage + bullet damage = final damage
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 5; // Delay between shots.
            Item.useAnimation = 5; // How long shoot animation lasts in ticks.
            Item.knockBack = 1f; // Gun knockback + bullet knockback = final knockback
            Item.autoReuse = true;

            // Other properties
            Item.value = 100000;
            Item.UseSound = SoundID.Item11; // Gun use sound

            // Gun properties
            Item.noMelee = true; // Item not dealing damage while held, we don’t hit mobs in the head with a gun
            Item.shoot = ProjectileID.PurificationPowder; // What kind of projectile the gun fires, does not mean anything here because it is replaced by ammo
            Item.shootSpeed = 5f; // Speed of a projectile. Mainly measured by eye
            Item.useAmmo = AmmoID.Bullet; // What ammo gun uses
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<FerretEye>(4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override Vector2? HoldoutOffset() => new Vector2(-8f, -4f); // Offset in pixels at which the player will hold the gun. -Y is up
    }
}