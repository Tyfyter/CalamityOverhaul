﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using CalamityMod.Projectiles.Ranged;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class SpykerHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "Spyker";
        public override int targetCayItem => ModContent.ItemType<Spyker>();
        public override int targetCWRItem => ModContent.ItemType<SpykerEcType>();

        public override void SetRangedProperty() {
            kreloadMaxTime = 90;
            FireTime = 15;
            HandDistance = 25;
            HandDistanceY = 5;
            HandFireDistance = 25;
            HandFireDistanceY = -6;
            ShootPosNorlLengValue = -2;
            ShootPosToMouLengValue = 10;
            RepeatedCartridgeChange = true;
            GunPressure = 0.1f;
            ControlForce = 0.05f;
            Recoil = 1.2f;
            RangeOfStress = 25;
            EnableRecoilRetroEffect = true;
            RecoilRetroForceMagnitude = 6;
        }

        public override void PreInOwnerUpdate() {
            LoadingAnimation(50, 3, 25);
        }

        public override void PostInOwnerUpdate() {
            base.PostInOwnerUpdate();
        }

        public override void FiringShoot() {
            SpawnGunFireDust(GunShootPos, ShootVelocity);
            if (AmmoTypes == ProjectileID.Bullet) {
                AmmoTypes = ModContent.ProjectileType<SpykerProj>();
            }
            Projectile.NewProjectile(Source, GunShootPos, ShootVelocity, AmmoTypes, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
        }

        public override void FiringShootR() {
            base.FiringShootR();
        }

        public override void PostFiringShoot() {
            base.PostFiringShoot();
        }
    }
}