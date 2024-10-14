﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class BladedgeGreatbowEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "BladedgeRailbow";
        public override void SetDefaults() {
            Item.SetItemCopySD<BladedgeRailbow>();
            Item.SetHeldProj<BladedgeGreatbowHeldProj>();
        }
    }
}
