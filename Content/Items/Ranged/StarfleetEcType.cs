﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class StarfleetEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "Starfleet";
        public override void SetDefaults() {
            Item.SetCalamityGunSD<Starfleet>();
            Item.SetCartridgeGun<StarfleetHeldProj>(60);
        }
    }
}