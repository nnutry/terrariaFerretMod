using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FerretMod.Content.Pets.LilyPet
{
    public class LilyPetItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.UnluckyYarn);
            Item.shoot = ModContent.ProjectileType<LilyPetProjectile>();
            Item.buffType = ModContent.BuffType<LilyPetBuff>();
        }
    
    public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
                player.AddBuff(Item.buffType, 2);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 4)
                //.AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}