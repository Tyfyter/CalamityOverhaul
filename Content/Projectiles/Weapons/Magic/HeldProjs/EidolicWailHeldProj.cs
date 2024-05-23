﻿using CalamityMod.Items.Weapons.Magic;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Magic;
using CalamityOverhaul.Content.Particles;
using CalamityOverhaul.Content.Particles.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Magic.HeldProjs
{
    internal class EidolicWailHeldProj : BaseMagicGun
    {
        public override string Texture => CWRConstant.Cay_Wap_Magic + "EidolicWail";
        public override int targetCayItem => ModContent.ItemType<EidolicWail>();
        public override int targetCWRItem => ModContent.ItemType<EidolicWailEcType>();
        public override void SetMagicProperty() {
            ShootPosToMouLengValue = 0;
            ShootPosNorlLengValue = 0;
            HandDistance = 20;
            HandDistanceY = 3;
            HandFireDistance = 20;
            HandFireDistanceY = -5;
            GunPressure = 0;
            ControlForce = 0;
            Recoil = 0;
            RecoilOffsetRecoverValue = 0.75f;
            SetRegenDelayValue = 60;
        }

        public override void PostInOwnerUpdate() {
            if (onFire) {
                //OffsetPos += CWRUtils.randVr(0.5f + (Item.useTime - GunShootCoolingValue) * 0.03f);
                if (Time % 10 == 0) {
                    Vector2 spanPos = Main.MouseWorld;
                    spanPos.X += Main.rand.Next(-260, 260);
                    spanPos.Y += Main.rand.Next(60, 100);
                    Vector2 vr = new Vector2(0, -Main.rand.Next(3, 19));
                    CWRParticle pulse3 = new DimensionalWave(spanPos, vr, Color.BlueViolet
                    , new Vector2(0.7f, 1.3f) * 0.8f, vr.ToRotation(), 0.18f, 0.32f, 60);
                    CWRParticleHandler.AddParticle(pulse3);
                }
            }
        }

        public override void FiringShoot() {
            OffsetPos += ShootVelocity.UnitVector() * -23;
            for (int i = 0; i < 13; i++) {
                Vector2 rand = CWRUtils.randVr(480, 800);
                Vector2 pos = Main.MouseWorld + rand;
                Vector2 vr = rand.UnitVector() * -ScaleFactor;
                if (CWRUtils.GetTile(pos / 16).HasSolidTile()) {
                    pos = Projectile.Center;
                    vr = ShootVelocity * Main.rand.NextFloat(0.3f, 1.13f);
                }
                Projectile proj = Projectile.NewProjectileDirect(Source, pos, vr, Item.shoot, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
                proj.scale += Main.rand.NextFloat(-0.2f, 0.2f);
                proj.velocity *= Main.rand.NextFloat(1, 1.13f);

                CWRParticle pulse3 = new DimensionalWave(GunShootPos, ShootVelocity * (0.3f + i * 0.1f), Color.BlueViolet
                , new Vector2(0.7f, 1.3f) * 0.8f, ShootVelocity.ToRotation(), 0.18f, 0.22f + i * 0.05f, 40);
                CWRParticleHandler.AddParticle(pulse3);
            }
        }
    }
}
