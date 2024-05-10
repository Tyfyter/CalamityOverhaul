﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs
{
    internal class CosmicBolterHeldProj : BaseBow
    {
        public override bool IsLoadingEnabled(Mod mod) {
            return false;//TODO:这个项目已经废弃，等待移除或者重做为另一个目标的事项
        }
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "CosmicBolter";
        //public override int targetCayItem => ModContent.ItemType<CosmicBolter>();
        //public override int targetCWRItem => ModContent.ItemType<CosmicBolterEcType>();
        int fireIndex;
        int fireIndex2;
        bool fire;
        public override void SetRangedProperty() {
            BowArrowDrawNum = 3;
        }
        public override void BowShoot() {
            Item.useTime = 20;
            if (fire) {
                Item.useTime = 5;
                fireIndex++;
                if (fireIndex >= 5) {
                    fire = false;
                    fireIndex = 0;
                }
            }
            if (AmmoTypes == ProjectileID.WoodenArrowFriendly) {
                //AmmoTypes = ModContent.ProjectileType<LunarBolt2>();
            }
            for (int i = 0; i < 3; i++) {
                FireOffsetPos = ShootVelocity.GetNormalVector() * ((-1 + i) * 8);
                base.BowShoot();
            }
            if (!fire) {
                if (++fireIndex2 > 12) {
                    fire = true;
                    fireIndex2 = 0;
                }
            }
        }
    }
}
