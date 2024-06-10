﻿using CalamityMod;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class MidasPrimeHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "MidasPrime";
        public override int targetCayItem => ModContent.ItemType<MidasPrime>();
        public override int targetCWRItem => ModContent.ItemType<MidasPrimeEcType>();

        private bool nextShotGoldCoin = false;
        public override void SetRangedProperty() {
            kreloadMaxTime = 90;
            FireTime = 22;
            HandDistance = 25;
            HandDistanceY = 5;
            HandFireDistance = 25;
            HandFireDistanceY = -5;
            ShootPosNorlLengValue = -10;
            ShootPosToMouLengValue = 10;
            RepeatedCartridgeChange = true;
            GunPressure = 0.3f;
            ControlForce = 0.05f;
            Recoil = 1.2f;
            RangeOfStress = 25;
            CanRightClick = true;
        }

        public override void PreInOwnerUpdate() {
            CanUpdateMagazineContentsInShootBool = CanCreateRecoilBool = onFire;
        }

        public override void SetShootAttribute() {
            FireTime = onFireR ? 12 : 22;
            CanCreateCaseEjection = CanCreateSpawnGunDust = onFire;
        }

        public override void HanderPlaySound() {
            if (onFireR) {
                SoundEngine.PlaySound(new("CalamityMod/Sounds/Custom/Ultrabling") 
                { PitchVariance = 0.5f }, Projectile.Center);
                return;
            }
            SoundEngine.PlaySound(Item.UseSound, Projectile.Center);
        }

        public override void FiringShoot() {
            if (AmmoTypes == ProjectileID.Bullet) {
                AmmoTypes = ModContent.ProjectileType<MarksmanShot>();
            }
            Projectile.NewProjectile(Source, GunShootPos, ShootVelocity
                , AmmoTypes, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
        }

        public override void FiringShootR() {
            long cashAvailable2 = Utils.CoinsCount(out bool overflow2, Owner.inventory);
            if (cashAvailable2 < 100 && !overflow2) {
                return;
            }
            if (Owner.GetActiveRicoshotCoinCount() >= 4) {
                return;
            }

            long cashAvailable = Utils.CoinsCount(out bool overflow, Owner.inventory);

            if (overflow || cashAvailable > 10000) {
                Owner.BuyItem(10000);
                nextShotGoldCoin = true;
            }
            else {
                Owner.BuyItem(100);
                nextShotGoldCoin = false;
            }

            float coinAIVariable = nextShotGoldCoin ? 2f : 1f;

            Projectile.NewProjectile(Source, GunShootPos, Owner.GetCoinTossVelocity()
                , ModContent.ProjectileType<RicoshotCoin>()
                , WeaponDamage, WeaponKnockback, Owner.whoAmI, coinAIVariable);
        }
    }
}
