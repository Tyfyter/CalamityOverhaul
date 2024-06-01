﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class ChickenCannonHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "ChickenCannon";
        public override int targetCayItem => ModContent.ItemType<ChickenCannon>();
        public override int targetCWRItem => ModContent.ItemType<ChickenCannonEcType>();
        bool spanSound = false;
        public override void SetRangedProperty() {
            kreloadMaxTime = 120;
            FireTime = 20;
            HandDistance = 25;
            HandDistanceY = 5;
            HandFireDistance = 25;
            HandFireDistanceY = -10;
            ShootPosNorlLengValue = -12;
            ShootPosToMouLengValue = 30;
            GunPressure = 0.3f;
            ControlForce = 0.05f;
            Recoil = 1.2f;
            RangeOfStress = 25;
            RecoilRetroForceMagnitude = 13;
            CanRightClick = true;
            FiringDefaultSound = false;
            RepeatedCartridgeChange = true;
            EnableRecoilRetroEffect = true;
        }

        public override void PostInOwnerUpdate() {
            CanUpdateMagazineContentsInShootBool = CanCreateRecoilBool = onFire;
        }

        public override void SetShootAttribute() {
            if (onFire) {
                GunPressure = 0.3f;
                ControlForce = 0.05f;
                RecoilRetroForceMagnitude = 13;
            }
            else if (onFireR) {
                GunPressure = 0;
                ControlForce = 0;
                RecoilRetroForceMagnitude = 0;
            }
        }

        public override void HanderPlaySound() {
            if (onFire) {
                SoundEngine.PlaySound(SoundID.Item61, Owner.Center);
            }
        }

        public override void FiringShoot() {
            Projectile.NewProjectile(Source, GunShootPos, ShootVelocity, Item.shoot
                , WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
        }

        public override void FiringShootR() {
            for (int i = 0; i < Main.maxProjectiles; ++i) {
                Projectile p = Main.projectile[i];
                if (!p.active || p.owner != Owner.whoAmI || p.type != Item.shoot) {
                    continue;
                }
                p.timeLeft = 1;
                p.netUpdate = true;
                p.netSpam = 0;
                spanSound = true;
            }
        }

        public override void PostFiringShoot() {
            if (spanSound) {
                RecoilRetroForceMagnitude = 22;
                SoundEngine.PlaySound(SoundID.Item110, Owner.Center);
            }
        }
    }
}
