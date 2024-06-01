﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class SandstormGunHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "SandstormGun";
        public override int targetCayItem => ModContent.ItemType<SandstormGun>();
        public override int targetCWRItem => ModContent.ItemType<SandstormGunEcType>();

        public override void SetRangedProperty() {
            kreloadMaxTime = 90;
            FireTime = 15;
            HandDistance = 25;
            HandDistanceY = 5;
            HandFireDistance = 25;
            HandFireDistanceY = -10;
            ShootPosNorlLengValue = -8;
            ShootPosToMouLengValue = 30;
            RepeatedCartridgeChange = true;
            GunPressure = 0;
            ControlForce = 0;
            Recoil = 0;
            RangeOfStress = 0;
            AmmoTypeAffectedByMagazine = false;
            EnableRecoilRetroEffect = false;
        }

        public override void FiringShoot() {
            Projectile.NewProjectile(Source, new Vector2(GunShootPos.X, Main.MouseWorld.Y), new Vector2(ShootVelocity.X, 0)
                , ModContent.ProjectileType<SandnadoOnSpan>(), WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
        }
    }
}
