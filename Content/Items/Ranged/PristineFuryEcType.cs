﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class PristineFuryEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "PristineFury";
        public override void SetDefaults() {
            Item.SetItemCopySD<PristineFury>();
            Item.SetCartridgeGun<PristineFuryHeldProj>(160);
            Item.CWR().CartridgeType = CartridgeUIEnum.JAR;
        }
    }
}
