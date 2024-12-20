﻿using CalamityMod.Items.Weapons.Typeless;
using CalamityOverhaul.Content.Projectiles.Weapons.Typeless;

namespace CalamityOverhaul.Content.Items.Typeless
{
    /// <summary>
    /// 马格努斯之眼
    /// </summary>
    internal class EyeofMagnusEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Typeless + "EyeofMagnus";
        public override void SetDefaults() {
            Item.SetItemCopySD<EyeofMagnus>();
            Item.SetHeldProj<EyeofMagnusHeldProj>();
        }
    }
}
