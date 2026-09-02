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
            Main.projFrames[Type] = 5;
            Main.projPet[Type] = true;

            ProjectileID.Sets.CharacterPreviewAnimations[Type] =
                ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Type], 6) // 6 тиков на кадр — подбери по вкусу
                    .WithOffset(-10, -20f)
                    .WithSpriteDirection(-1)
                    .WithCode(DelegateMethods.CharacterPreview.Float);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish); // наземный пёсик — уже умеет бегать/прыгать
            AIType = ProjectileID.ZephyrFish;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.zephyrfish = false; // Relic from AIType

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<LilyPetBuff>()))
                Projectile.timeLeft = 2;

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 30) // скорость анимации — тиков на кадр
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }
        }
    }
}
