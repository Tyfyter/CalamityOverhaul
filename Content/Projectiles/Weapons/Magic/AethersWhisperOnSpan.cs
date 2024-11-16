﻿using CalamityMod.Particles;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Magic
{
    internal class AethersWhisperOnSpan : BaseBloomOnSpan
    {
        public override void SetBloom() {
            color1 = Color.Blue;
            color2 = Color.AliceBlue;
            BloomSize = 0.3f;
            toMouLeng = 65;
            norlLeng = -2;
            MaxCharge = 30;
        }

        public override void SpanProjFuncInKill() {
            for (int i = 0; i < 13; i++) {
                SparkParticle spark2 = new SparkParticle(Projectile.Center + Main.rand.NextVector2Circular(12, 12)
                    , ((i / 13f) * MathHelper.TwoPi).ToRotationVector2() * 6, false, Main.rand.Next(2, 7)
                    , 0.5f, Main.rand.NextBool(4) ? color1 : color2);
                GeneralParticleHandler.SpawnParticle(spark2);
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center
                , Projectile.rotation.ToRotationVector2() * Owner.GetShootState().ShootSpeed
                , ModContent.ProjectileType<AetherOrb>()
                , Projectile.damage, Projectile.knockBack, Owner.whoAmI, 0);
        }
    }
}
