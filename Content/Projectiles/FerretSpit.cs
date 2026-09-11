using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace FerretMod.Content.Projectiles
{
    // PROJECTILE
    public class FerretSpit : ModProjectile
    {
        // My vars
        public int freeFly = 50;
        private int dustTimer = 0; // для брызгов в полете

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            // Visual properties
            Projectile.width = 12; // The width of projectile hitbox
            Projectile.height = 12; // The height of projectile hitbox
            Projectile.alpha = 200; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0f; // How much light emit around the projectile

            // Combat properties
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second)

            // Other properties
            Projectile.aiStyle = ProjAIStyleID.Arrow; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If collide with tile, reduce the penetrate.
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item54, Projectile.position);

                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Draws an afterimage trail. See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#afterimage-trail for more information.

            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            for (int i=0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 4f);
                speed.Y += 0.7f;

                Dust dust = Dust.NewDustPerfect(
                    Projectile.Center,      // Точка появления (центр снаряда)
                    DustID.Water,           // Тип частиц
                    speed,                  // Скорость и направление полета частицы (вектор)
                    0,                      // Альфа-канал
                    default,                // Кастомный цвет
                    Main.rand.NextFloat(0.5f, 1.5f) // Случайный размер капель
                );
                // Дополнительные свойства для физики капель
                dust.noGravity = false;
                dust.fadeIn = 0.5f;
            }
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
        }

        public override void AI()
        {
            // появление брызга в полете
            dustTimer++;
            if (dustTimer % 5 == 0)
            {
                // эффект плевочка, dust
                Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 4f);
                speed.Y += 1f;

                Dust dust = Dust.NewDustPerfect(
                    Projectile.Center,      // Точка появления (центр снаряда)
                    DustID.Water,           // Тип частиц
                    speed,                  // Скорость и направление полета частицы (вектор)
                    0,                      // Альфа-канал
                    default,                // Кастомный цвет
                    Main.rand.NextFloat(0.5f, 1.5f) // Случайный размер капель
                );
                // Дополнительные свойства для физики капель
                dust.noGravity = false;
                dust.fadeIn = 0.5f;
            }
            //------------------------------------------------------

            // небольшой штраф пока freeFly есть, потом большой
            if (freeFly > 0)
            {
                freeFly--;
                Projectile.velocity.Y += 0.01f;
                return;
            }
            else
            {
                Projectile.velocity.Y += 0.04f;
            }
            // Ограничение максимальной скорости падения
            if (Projectile.velocity.Y > 6f)
            {
                Projectile.velocity.Y = 6f;
            }
        }

    }
}