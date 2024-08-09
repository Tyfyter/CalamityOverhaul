﻿using CalamityMod.Particles;
using CalamityOverhaul.Content.Items.Ranged.Extras;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.Core;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class SpectreRifleHeldProj : BaseGun
    {
        public override string Texture => CWRConstant.Item_Ranged + "SpectreRifle";
        public override bool CheckAlive() {
            return Item.type == ModContent.ItemType<SpectreRifle>();
        }

        public override void SetRangedProperty() {
            ControlForce = 0.035f;
            GunPressure = 0.5f;
            Recoil = 2.5f;
            ShootPosNorlLengValue = -20;
            HandFireDistance = HandDistance = 80;
            HandDistanceY = 34;
            HandFireDistanceY = 8;
            CanCreateSpawnGunDust = false;
        }

        public override void FiringShoot() {
            Vector2 pos = Projectile.Center + ShootVelocity.UnitVector() * 33 + ShootVelocity.GetNormalVector() * 5 * DirSign;
            for (int i = 0; i < 12; i++) {
                int sparkLifetime = Main.rand.Next(22, 36);
                float sparkScale = Main.rand.NextFloat(1f, 1.5f);
                Color sparkColor = Color.WhiteSmoke;
                Vector2 sparkVelocity = ShootVelocity.RotatedBy(Main.rand.NextFloat(-0.1f, 0.1f)) * Main.rand.NextFloat(0.3f, 1.2f);
                SparkParticle spark = new SparkParticle(pos, sparkVelocity, false, sparkLifetime, sparkScale, sparkColor);
                GeneralParticleHandler.SpawnParticle(spark);
            }
            Projectile.NewProjectile(Source, Projectile.Center, ShootVelocity
                    , ModContent.ProjectileType<LostSoulBullet>(), WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);

            UpdateConsumeAmmo();
        }
    }
}
