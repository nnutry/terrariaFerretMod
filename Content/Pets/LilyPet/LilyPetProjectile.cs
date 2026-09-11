using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FerretMod.Content.Pets.LilyPet
{
    public class LilyPetProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 10;
            Main.projPet[Type] = true;

            ProjectileID.Sets.CharacterPreviewAnimations[Type] =
                ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Type], 60) // 6 тиков на кадр — подбери по вкусу
                    .WithOffset(-10, -20f)
                    .WithSpriteDirection(-1)
                    .WithCode(DelegateMethods.CharacterPreview.Float);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SugarGlider); // основа логики на ванильном звере
            AIType = ProjectileID.SugarGlider;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<LilyPetBuff>()))
                Projectile.timeLeft = 2;

            //if (Projectile.frameCounter >= 2) // скорость анимации — тиков на кадр
            //{
            //    Projectile.frameCounter = 0;
            //    Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            //}
        }
    }
}
