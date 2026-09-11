using FerretMod.Content.Items.Materials; // Using our Materials folder
using FerretMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace FerretMod.Content.Items.Weapons // Where is your code locates
{
    public class FerretSword : ModItem
    {
        // My vars
        int shootCount = 2;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; // How many items need for research in Journey Mode
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.width = 48; // Width of an item sprite
            Item.height = 48; // Height of an item sprite
            Item.scale = 1f; // Multiplicator of item size, for example is you set this to 2f our sword will be biger twice. IMPORTANT: If you are using numbers with floating point, write "f" in their end, like 1.5f, 3.14f, 2.1278495f etc.
            Item.rare = ItemRarityID.Blue; // The color of item's name in game. Check https://terraria.wiki.gg/wiki/Rarity

            // Combat properties
            Item.damage = 76; // Item damage
            Item.DamageType = DamageClass.Melee; // What type of damage item is deals, Melee, Ranged, Magic, Summon, Generic (takes bonuses from all damage multipliers), Default (doesn't take bonuses from any damage multipliers)
            // useTime and useAnimation often use the same value, but we'll see examples where they don't use the same values
            Item.useTime = 30; // How long the swing lasts in ticks (60 ticks = 1 second)
            Item.useAnimation = 30; // How long the swing animation lasts in ticks (60 ticks = 1 second)
            Item.knockBack = 10f; // How far the sword punches enemies, 20 is maximal value
            Item.autoReuse = true; // Can the item auto swing by holding the attack button
            // Shooting
            Item.shoot = ModContent.ProjectileType<FerretSpit>();
            Item.shootSpeed = 4f;


            // Other properties
            Item.value = Item.buyPrice(gold: 1); // Item sell price in copper coins
            Item.useStyle = ItemUseStyleID.Swing; // This is how you're holding the weapon, visit https://terraria.wiki.gg/wiki/Use_Style_IDs for list of possible use styles
            Item.UseSound = SoundID.Item85; // What sound is played when using the item, all sounds can be found here - https://terraria.wiki.gg/wiki/Sound_IDs
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shootCount++;

            if (shootCount % 3 == 0)
            {
                shootCount = 0;
                return true;
            }
            
            return false;
        }

        // Creating item craft
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FerretEye>(7); // We are using custom material for the craft, 7 Steel Shards
            recipe.AddIngredient(ItemID.Wood, 3); // Also, we are using vanilla material to craft, 3 Wood
            recipe.AddTile(TileID.WorkBenches); // Crafting station we need for craft, WorkBenches, Anvils etc. You can find them here - https://terraria.wiki.gg/wiki/Tile_IDs
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3)) // With 1/3 chance per tick (60 ticks = 1 second)...
            {
                // ...spawning dust
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), // Position to spawn
                (int)(hitbox.Width * 1.2f), (int)(hitbox.Height * 1.2), // Width and Height
                DustID.Poisoned, // Dust type. Check https://terraria.wiki.gg/wiki/Dust_IDs
                0, 0, // Speed X and Speed Y of dust, it have some randomization
                125); // Dust transparency, 0 - full visibility, 255 - full transparency

            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4)) // 1/4 chance, or 25% in other words
            {
                target.AddBuff(BuffID.Poisoned, // Adding Poisoned to target
                    300); // for 5 seconds (60 ticks = 1 second)
            }
        }
    }
}