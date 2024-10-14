﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class MolecularManipulatorEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "MolecularManipulator";
        public override void SetDefaults() {
            Item.SetItemCopySD<MolecularManipulator>();
            Item.SetCartridgeGun<MolecularManipulatorHeldProj>(480);
        }
    }
}
