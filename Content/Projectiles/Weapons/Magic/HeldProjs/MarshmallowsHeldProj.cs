﻿using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Magic.Extras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Magic.HeldProjs
{
    internal class MarshmallowsHeldProj : BaseMagicGun
    {
        public override string Texture => CWRConstant.Item_Magic + "Marshmallow2";
        public override int targetCayItem => ModContent.ItemType<Marshmallows>();
        public override int targetCWRItem => ModContent.ItemType<Marshmallows>();
        public override void SetMagicProperty() {
            ShootPosToMouLengValue = 30;
            ShootPosNorlLengValue = -20;
            HandDistance = 0;
            HandDistanceY = 0;
            HandFireDistance = 0;
            HandFireDistanceY = -0;
            GunPressure = 0;
            ControlForce = 0;
            Recoil = 0;
        }

        public override void FiringShoot() {
            for (int i = 0; i < 3; i++) {
                if (Owner.CheckMana(Item)) {
                    Projectile.NewProjectile(Source, GunShootPos, ShootVelocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(0.3f, 1.1f)
                        , Item.shoot, WeaponDamage, WeaponKnockback, Owner.whoAmI, 0);
                    Owner.statMana -= Item.mana;
                }
            }
        }

        public override void GunDraw(ref Color lightColor) {
            Main.EntitySpriteDraw(TextureValue, Projectile.Center - Main.screenPosition, null, lightColor
                , Projectile.rotation, DirSign > 0 ? new Vector2(10, 68) : new Vector2(10, 10), Projectile.scale, DirSign > 0 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
    }
}
