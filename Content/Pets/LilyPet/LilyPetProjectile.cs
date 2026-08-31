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
            Main.projFrames[Projectile.type] = 2;
            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] =
                ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Projectile.type] - 1, 6) // 6 тиков на кадр — подбери по вкусу
                    .WithOffset(-10, -20f)
                    .WithSpriteDirection(-1)
                    .WithCode(DelegateMethods.CharacterPreview.BerniePet);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Puppy); // наземный пёсик — уже умеет бегать/прыгать
            AIType = ProjectileID.Puppy;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<LilyPetBuff>()))
                Projectile.timeLeft = 2;
        }
    }
}
