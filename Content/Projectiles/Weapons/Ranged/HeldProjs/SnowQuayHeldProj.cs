﻿using CalamityOverhaul.Content.Items.Ranged.Extras;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.Core;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class SnowQuayHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Item_Ranged + "SnowQuayHeld";
        public override int targetCayItem => ModContent.ItemType<SnowQuay>();
        public override int targetCWRItem => ModContent.ItemType<SnowQuay>();

        private int fireIndex;
        public override void SetRangedProperty() {
            Recoil = 0.3f;
            FireTime = 10;
            GunPressure = 0;
            HandDistance = 42;
            HandDistanceY = 0;
            HandFireDistance = 42;
            HandFireDistanceY = -10;
            AngleFirearmRest = -1;
            ShootPosNorlLengValue = 0;
            ShootPosToMouLengValue = 20;
            RecoilRetroForceMagnitude = 5;
            EnableRecoilRetroEffect = true;
            CanCreateCaseEjection = false;
        }

        public override void PostInOwnerUpdate() {
            if (DownLeft && !Owner.mouseInterface && IsKreload) {
                fireIndex++;
                if (fireIndex < 90) {
                    CWRUtils.ClockFrame(ref Projectile.frame, 2, 3);
                    SoundEngine.PlaySound(SoundID.Item23 with { Pitch = (90 - fireIndex) * 0.15f, MaxInstances = 13, Volume = 0.2f + fireIndex * 0.006f }, Projectile.Center);
                    FiringDefaultSound = EnableRecoilRetroEffect = false;
                    ShootCoolingValue = 2;
                }
                else {
                    Projectile.frameCounter++;
                    if (Projectile.frameCounter >= 2) {
                        Projectile.frame++;
                        if (Projectile.frame > 5) {
                            Projectile.frame = 4;
                        }
                        Projectile.frameCounter = 0;
                    }
                    FiringDefaultSound = EnableRecoilRetroEffect = true;
                }
            }
            else {
                fireIndex = 0;
                Projectile.frame = 0;
            }
        }

        public override void HanderSpwanDust() {
            SpawnGunFireDust(GunShootPos, ShootVelocity, splNum: 1, dustID1: 76, dustID2: 149, dustID3: 76);
        }

        public override void FiringShoot() {
            int proj = Projectile.NewProjectile(Source, GunShootPos, ShootVelocity, ModContent.ProjectileType<SnowQuayBall>(), WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
            Main.projectile[proj].ArmorPenetration = 10;
        }

        public override void GunDraw(Vector2 drawPos, ref Color lightColor) {
            Main.EntitySpriteDraw(TextureValue, drawPos, CWRUtils.GetRec(TextureValue, Projectile.frame, 6), lightColor
                , Projectile.rotation, CWRUtils.GetOrig(TextureValue, 6), Projectile.scale
                , DirSign > 0 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
    }
}
