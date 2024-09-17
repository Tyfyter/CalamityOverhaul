﻿using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.OtherMods.Thorium.Core
{
    internal class FromThorium : ILoader
    {
        public const string Name = "ThoriumMod";
        public static bool Has => ModLoader.HasMod(Name);
        public static List<LThoriumCall> lThoriumCalls = [];
        void ILoader.LoadData() {
            if (!Has) return;
            CWRMod.Instance.thoriumMod = ModLoader.GetMod(Name);
            lThoriumCalls = CWRUtils.GetSubInterface<LThoriumCall>("LThoriumCall");
            foreach (var call in lThoriumCalls) {
                call.LoadThoData(CWRMod.Instance.thoriumMod);
            }
        }

        void ILoader.UnLoadData() {
            if (lThoriumCalls != null) {
                foreach (var call in lThoriumCalls) {
                    call.UnLoadThoData();
                }
            }
            lThoriumCalls = null;
        }

        void ILoader.SetupData() {
            if (!Has) return;
            foreach (var call in lThoriumCalls) {
                call.PostLoadThoData(CWRMod.Instance.thoriumMod);
            }
        }
    }
}
