﻿using CalamityMod;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class SpectralstormCannonHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "SpectralstormCannon";
        public override int targetCayItem => ModContent.ItemType<SpectralstormCannon>();
        public override int targetCWRItem => ModContent.ItemType<SpectralstormCannonEcType>();

        private int fireIndex;
        public override void SetRangedProperty() {
            kreloadMaxTime = 90;
            FireTime = 20;
            HandDistance = 25;
            HandDistanceY = 5;
            HandFireDistance = 25;
            HandFireDistanceY = -10;
            ShootPosNorlLengValue = 2;
            ShootPosToMouLengValue = 20;
            RepeatedCartridgeChange = true;
            GunPressure = 0.1f;
            ControlForce = 0.05f;
            Recoil = 1.2f;
            RangeOfStress = 25;
            EnableRecoilRetroEffect = true;
            RecoilRetroForceMagnitude = 6;
        }

        public override void FiringShoot() {
            if (fireIndex > 25) {
                FireTime = 20;
                fireIndex = 0;
            }
            for (int index = 0; index < 5; ++index) {
                Vector2 velocity = ShootVelocity;
                velocity.X += Main.rand.Next(-40, 41) * 0.05f;
                velocity.Y += Main.rand.Next(-40, 41) * 0.05f;
                int proj = Projectile.NewProjectile(Source, GunShootPos, velocity, AmmoTypes, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
                if (proj.WithinBounds(Main.maxProjectiles)) {
                    Main.projectile[proj].timeLeft = 150;
                    Main.projectile[proj].DamageType = DamageClass.Ranged;
                }
            }
            for (int index = 0; index < 5; ++index) {
                Vector2 velocity = ShootVelocity;
                velocity *= Main.rand.NextFloat(1, 1.1f);
                velocity.X += Main.rand.Next(-20, 21) * 0.05f;
                velocity.Y += Main.rand.Next(-20, 21) * 0.05f;
                int soul = Projectile.NewProjectile(Source, GunShootPos, velocity, ModContent.ProjectileType<LostSoulFriendly>(), WeaponDamage, WeaponKnockback, Owner.whoAmI, 2f, 0f);
                if (soul.WithinBounds(Main.maxProjectiles)) {
                    Main.projectile[soul].timeLeft = 600;
                    Main.projectile[soul].DamageType = DamageClass.Ranged;
                    Main.projectile[soul].frame = Main.rand.Next(4);
                }
            }
            FireTime--;
            if (FireTime < 5) {
                FireTime = 5;
            }
            fireIndex++;
        }
    }
}
