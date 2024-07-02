﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class VoidragonEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "Voidragon";
        public override void SetDefaults() {
            Item.SetCalamitySD<Voidragon>();
            Item.SetCartridgeGun<VoidragonHeldProj>(800);
        }
    }
}
