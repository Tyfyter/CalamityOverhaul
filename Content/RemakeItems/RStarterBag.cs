﻿using CalamityMod;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Pets;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Items;
using CalamityOverhaul.Content.Items.Magic.Extras;
using CalamityOverhaul.Content.Items.Materials;
using CalamityOverhaul.Content.Items.Ranged.Extras;
using CalamityOverhaul.Content.Items.Summon.Extras;
using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems
{
    internal class RStarterBag : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<StarterBag>();
        public override bool FormulaSubstitution => false;
        public override bool CanLoad() {//如果不启用开局物品修改，那么便不会试图修改开局战利品袋子的内容
            return CWRServerConfig.Instance.OpeningOukModification;
        }
        internal void Loot(ItemLoot itemLoot) {
            itemLoot.Add(ModContent.ItemType<OverhaulTheBibleBook>());
            itemLoot.Add(ModContent.ItemType<TheUpiStele>());
            itemLoot.Add(ModContent.ItemType<TheSpiritFlint>());
            itemLoot.Add(ModContent.ItemType<Slingshot>());
            itemLoot.Add(ModContent.ItemType<Pebble>(), 1, 350, 450);
            itemLoot.Add(ItemID.WoodenArrow, 1, 100, 100);
            itemLoot.Add(ItemID.ManaCrystal);

            itemLoot.Add(ItemID.Bomb, 1, 10, 10);
            itemLoot.Add(ItemID.Rope, 1, 50, 50);
            itemLoot.Add(ItemID.MiningPotion);
            itemLoot.Add(ItemID.SpelunkerPotion, 1, 2, 2);
            itemLoot.Add(ItemID.SwiftnessPotion, 1, 3, 3);
            itemLoot.Add(ItemID.GillsPotion, 1, 2, 2);
            itemLoot.Add(ItemID.ShinePotion);
            itemLoot.Add(ItemID.RecallPotion, 1, 3, 3);
            itemLoot.Add(ItemID.Torch, 1, 25, 25);
            itemLoot.Add(ItemID.Chest, 1, 3, 3);

            Mod musicMod = CWRMod.Instance.musicMod;
            if (musicMod is not null)
                itemLoot.Add(musicMod.Find<ModItem>("CalamityMusicbox").Type);
            itemLoot.Add(ModContent.ItemType<LoreAwakening>());

            static bool getsLadPet(DropAttemptInfo info) {
                string playerName = info.player.name;
                return playerName == "Aleksh" || playerName == "Shark Lad";
            };
            itemLoot.AddIf(getsLadPet, ModContent.ItemType<JoyfulHeart>());
            static bool getsHapuFruit(DropAttemptInfo info) {
                string playerName = info.player.name;
                return playerName == "Heart Plus Up";
            };
            itemLoot.AddIf(getsHapuFruit, ModContent.ItemType<HapuFruit>());
            static bool getsRedBow(DropAttemptInfo info) {
                string playerName = info.player.name;
                return playerName == "Pelusa";
            }
            itemLoot.AddIf(getsRedBow, ModContent.ItemType<RedBow>());
            static bool getsOracleHeadphones(DropAttemptInfo info) {
                string playerName = info.player.name;
                return playerName is "Amber" or "Mishiro";
            }
            itemLoot.AddIf(getsOracleHeadphones, ModContent.ItemType<OracleHeadphones>());
            static bool getsSakuraFeather(DropAttemptInfo info) {
                string playerName = info.player.name;
                return playerName == "bird";
            }
            itemLoot.AddIf(getsSakuraFeather, ModContent.ItemType<CocosFeather>());
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            itemLoot.RemoveWhere(i => true);
            Loot(itemLoot);
        }
        //为了更加方便的修改掉落内容，这里直接使用On钩子阻断运行
        //以此来进行一些具体的修改
        //TODO:这个方法目前无作用，因为未知原因钩子不再正常运行以达到想要的效果
        //public override bool? On_ModifyItemLoot(Item item, ItemLoot itemLoot) {
        //    Loot(itemLoot);
        //    return false;
        //}
    }
}
