﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class GunkShotEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "GunkShot";
        public override void SetDefaults() {
            Item.SetItemCopySD<GunkShot>();
            Item.SetCartridgeGun<GunkShotHeldProj>(10);
        }
    }
}
