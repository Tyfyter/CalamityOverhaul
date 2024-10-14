﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class FirestormCannonEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "FirestormCannon";
        public override void SetDefaults() {
            Item.SetItemCopySD<FirestormCannon>();
            Item.SetCartridgeGun<FirestormCannonHeldProj>(60);
        }
    }
}
