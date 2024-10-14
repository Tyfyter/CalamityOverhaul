﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class StarSputterEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "StarSputter";
        public override void SetDefaults() {
            Item.SetItemCopySD<StarSputter>();
            Item.SetCartridgeGun<StarSputterHeldProj>(42);
        }
    }
}
