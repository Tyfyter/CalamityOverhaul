﻿using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged
{
    internal class TyphoonOnSpan : BaseOnSpanProj
    {
        protected override Color[] colors => new Color[] { Color.LightSkyBlue, Color.Blue, Color.AliceBlue, Color.LightBlue };
        protected override float halfSpreadAngleRate => 1.15f;
        protected override float edgeBlendLength => 0.17f;
        protected override float edgeBlendStrength => 9;
        public override float MaxCharge => 180;

        public override void SpanProjFunc() {
            int arrowTypes;
            int weaponDamage;
            float weaponKnockback;
            bool haveAmmo = Owner.PickAmmo(Owner.ActiveItem(), out arrowTypes, out _, out weaponDamage, out weaponKnockback, out _, false);
            if (haveAmmo && !CalamityUtils.CheckWoodenAmmo(arrowTypes, Owner)) {
                for (int i = 0; i < 64; i++) {
                    float rot = MathHelper.TwoPi / 64 * i;
                    Vector2 velocity = rot.ToRotationVector2() * (19 + (-11f + rot % MathHelper.PiOver4) * 17);
                    Projectile.NewProjectile(Owner.parent(), Projectile.Center, velocity
                        , arrowTypes, weaponDamage, weaponKnockback, Owner.whoAmI);
                }
                return;
            }
            weaponDamage = Projectile.damage;
            weaponKnockback = Projectile.knockBack;
            for (int i = 0; i < 94; i++) {
                float rot = MathHelper.TwoPi / 94 * i;
                Vector2 velocity = rot.ToRotationVector2() * (1 + (-6f + rot % MathHelper.PiOver4) * 2);
                Projectile.NewProjectile(Owner.parent(), Projectile.Center, velocity
                    , ModContent.ProjectileType<TorrentialArrow>()
                    , weaponDamage, weaponKnockback, Owner.whoAmI);
            }
        }
    }
}
