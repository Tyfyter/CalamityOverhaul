﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class ContinentalGreatbowEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "ContinentalGreatbow";
        public override void SetDefaults() {
            Item.SetItemCopySD<ContinentalGreatbow>();
            Item.SetHeldProj<ContinentalGreatbowHeldProj>();
        }
    }
}
