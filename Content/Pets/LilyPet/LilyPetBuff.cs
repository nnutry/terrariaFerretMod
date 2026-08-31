using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FerretMod.Content.Pets.LilyPet
{
    public class LilyPetBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LilyPetProjectile>()] <= 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(player.GetSource_Buff(buffIndex),
                        player.Center, Vector2.Zero,
                        ModContent.ProjectileType<LilyPetProjectile>(),
                        0, 0f, player.whoAmI);
                }
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}