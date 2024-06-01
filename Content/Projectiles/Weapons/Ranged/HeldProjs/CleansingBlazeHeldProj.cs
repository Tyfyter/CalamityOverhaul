﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class CleansingBlazeHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "CleansingBlaze";
        public override int targetCayItem => ModContent.ItemType<CleansingBlaze>();
        public override int targetCWRItem => ModContent.ItemType<CleansingBlazeEcType>();
        private int fireIndex;
        public override void SetRangedProperty() {
            FireTime = 3;
            HandDistance = 25;
            HandDistanceY = 4;
            HandFireDistance = 25;
            HandFireDistanceY = -5;
            ShootPosNorlLengValue = -3;
            ShootPosToMouLengValue = 0;
            GunPressure = 0;
            ControlForce = 0;
            Recoil = 0.5f;
            RangeOfStress = 28;
            RepeatedCartridgeChange = true;
            FiringDefaultSound = false;
            kreloadMaxTime = 90;
            loadTheRounds = CWRSound.Liquids_Fill_0 with { Pitch = -0.8f };
            LoadingAA_None.loadingAA_None_Roting = 30;
            LoadingAA_None.loadingAA_None_X = 0;
            LoadingAA_None.loadingAA_None_Y = 13;
        }

        public override void SetShootAttribute() {
            if (++fireIndex > 5) {
                SoundEngine.PlaySound(Item.UseSound, Projectile.Center);
                fireIndex = 0;
            }
        }

        public override void FiringShoot() {
            int num6 = Main.rand.Next(2, 4);
            for (int index = 0; index < num6; ++index) {
                float SpeedX = ShootVelocity.X + (Main.rand.Next(-15, 16) * 0.05f);
                float SpeedY = ShootVelocity.Y + (Main.rand.Next(-15, 16) * 0.05f);
                _ = Projectile.NewProjectile(Source, GunShootPos.X, GunShootPos.Y
                    , SpeedX, SpeedY, Item.shoot, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0f, 0f);
            }
        }
    }
}
