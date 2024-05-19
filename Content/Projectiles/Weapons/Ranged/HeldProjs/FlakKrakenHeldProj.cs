﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class FlakKrakenHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "FlakKraken";
        public override int targetCayItem => ModContent.ItemType<FlakKraken>();
        public override int targetCWRItem => ModContent.ItemType<FlakKrakenEcType>();
        int fireIndex;
        public override void SetRangedProperty() {
            kreloadMaxTime = 90;
            FireTime = 10;
            HandDistance = 25;
            HandDistanceY = 5;
            HandFireDistance = 25;
            HandFireDistanceY = -5;
            ShootPosNorlLengValue = -2;
            ShootPosToMouLengValue = 10;
            RepeatedCartridgeChange = true;
            GunPressure = 0;
            ControlForce = 0;
            Recoil = 1.2f;
            RangeOfStress = 25;
            AmmoTypeAffectedByMagazine = false;
            EnableRecoilRetroEffect = true;
            FiringDefaultSound = false;
            RecoilRetroForceMagnitude = 17;
            RecoilOffsetRecoverValue = 0.8f;
            LoadingAmmoAnimation = LoadingAmmoAnimationEnum.Handgun;
        }

        public override void PreInOwnerUpdate() {
            //LoadingAnimation(50, 3, 25);
        }

        public override void PostInOwnerUpdate() {
        }

        public override void FiringShoot() {
            FireTime = 8;
            RecoilRetroForceMagnitude = 17 + fireIndex;
            AmmoTypes = ModContent.ProjectileType<FlakKrakenProjectile>();
            Projectile.NewProjectile(Source, GunShootPos, ShootVelocityInProjRot, AmmoTypes, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0, ToMouse.Length());
            CaseEjection(1.3f);
            if (++fireIndex > 6) {
                FireTime = 30;
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/DudFire") with { Pitch = -0.7f, PitchVariance = 0.1f }, Projectile.Center);
                fireIndex = 0;
                return;
            }
            SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/FlakKrakenShoot") { Volume = 0.5f }, Projectile.Center);
        }
    }
}