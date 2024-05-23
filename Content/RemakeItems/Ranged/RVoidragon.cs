﻿using CalamityMod.Items.Weapons.Ranged;
using CalamityOverhaul.Content.Items.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;
using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Ranged
{
    internal class RVoidragon : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<Voidragon>();
        public override int ProtogenesisID => ModContent.ItemType<VoidragonEcType>();
        public override string TargetToolTipItemName => "VoidragonEcType";
        public override void SetDefaults(Item item) => item.SetCartridgeGun<VoidragonHeldProj>(800);
    }
}
