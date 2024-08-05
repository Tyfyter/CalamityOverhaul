﻿using CalamityOverhaul.Content.OtherMods.Thorium.Core;
using CalamityOverhaul.Content.OtherMods.Thorium.ProjectileSet.Helds;
using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.OtherMods.Thorium.ItemSet
{
    internal class RFrostFury : BaseRItem, LThoriumCall
    {
        internal static int FrostFuryID;
        public override int TargetID => FrostFuryID;
        public override bool FormulaSubstitution => false;
        public override bool CanLoad() => FromThorium.Has;
        public void LoadThoData(Mod thoriumMod) { }
        public void PostLoadThoData(Mod thoriumMod) => FrostFuryID = thoriumMod.Find<ModItem>("FrostFury").Type;
        public override void SetDefaults(Item item) => item.SetHeldProj<FrostFuryHeldProj>();
    }
}
