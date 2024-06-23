﻿using CalamityMod.Items.Weapons.Magic;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items.Magic;
using CalamityOverhaul.Content.Projectiles.Weapons.Magic.HeldProjs;
using CalamityOverhaul.Content.RemakeItems.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Magic
{
    internal class RSHPC : BaseRItem, ISetupData
    {
        public static MethodInfo methodInfo;
        public override int TargetID => ModContent.ItemType<SHPC>();
        public override int ProtogenesisID => ModContent.ItemType<SHPCEcType>();
        public override string TargetToolTipItemName => "";
        private static void onSHPCToolFunc(RItemSystem.On_ModItem_ModifyTooltips_Delegate orig, object obj, List<TooltipLine> list) { }
        void ISetupData.LoadData() {
            methodInfo = typeof(SHPC).GetMethod("ModifyTooltips", BindingFlags.Public | BindingFlags.Instance);
            MonoModHooks.Add(methodInfo, onSHPCToolFunc);
        }
        void ISetupData.UnLoadData() => methodInfo = null;
        public override void SetDefaults(Item item) {
            item.damage = SHPCEcType.GetStartDamage;
            item.SetHeldProj<SHPCHeldProj>();
        }
        public override bool On_ModifyWeaponDamage(Item item, Player player, ref StatModifier damage) {
            SHPCEcType.SHPCDamage(ref damage);
            return false;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            List<TooltipLine> newTooltips = new List<TooltipLine>(tooltips);
            List<TooltipLine> prefixTooltips = new List<TooltipLine>();
            foreach (TooltipLine line in newTooltips.ToList()) {
                for (int i = 0; i < 9; i++) {
                    if (line.Name == "Tooltip" + i) {
                        line.Hide();
                    }
                }
                if (line.Name.Contains("Prefix")) {
                    prefixTooltips.Add(line.Clone());
                    line.Hide();
                }
            }

            string textContent = Language.GetText("Mods.CalamityOverhaul.Items.SHPCEcType.Tooltip").Value;
            string[] legendtopsList = textContent.Split("\n");
            foreach (string legendtops in legendtopsList) {
                string text = legendtops;
                int index = InWorldBossPhase.Instance.SHPC_Level();
                TooltipLine newLine = new TooltipLine(CWRMod.Instance, "CWRText", text);
                if (newLine.Text == "[Text]") {
                    if (index >= 0 && index <= 14) {
                        text = CWRLocText.GetTextValue($"SHPC_TextDictionary_Content_{index}");
                    }
                    else {
                        text = "ERROR";
                    }

                    if (!CWRServerConfig.Instance.WeaponEnhancementSystem) {
                        text = InWorldBossPhase.Instance.level11 ? CWRLocText.GetTextValue("SHPC_No_legend_Content_2") : CWRLocText.GetTextValue("SHPC_No_legend_Content_1");
                    }
                    newLine.Text = text;
                    // 使用颜色渐变以提高可读性
                    newLine.OverrideColor = Color.Lerp(Color.BlueViolet, Color.White, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f);
                }
                // 将新提示行添加到新集合中
                newTooltips.Add(newLine);
            }
            SHPCEcType.SetTooltip(ref newTooltips, CWRMod.Instance.Name);
            // 清空原 tooltips 集合并添加修改后的新Tooltips集合
            tooltips.Clear();
            tooltips.AddRange(newTooltips);
            tooltips.AddRange(prefixTooltips);
        }
    }
}
