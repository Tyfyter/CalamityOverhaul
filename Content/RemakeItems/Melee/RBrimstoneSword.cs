﻿using CalamityMod;
using CalamityMod.Items;
using CalamityOverhaul.Content.Items.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee;
using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Melee
{
    internal class RBrimstoneSword : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<CalamityMod.Items.Weapons.Melee.BrimstoneSword>();
        public override int ProtogenesisID => ModContent.ItemType<BrimstoneSwordEcType>();
        public override string TargetToolTipItemName => "BrimstoneSwordEcType";

        public override void SetDefaults(Item item) {
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.Rapier;
            item.damage = 98;
            item.DamageType = DamageClass.Melee;
            item.useAnimation = item.useTime = 10;
            item.shoot = ModContent.ProjectileType<BrimstoneSwordHeldProj>();
            item.shootSpeed = 2f;
            item.knockBack = 7.5f;
            item.UseSound = SoundID.Item1;
            item.value = CalamityGlobalItem.RarityPinkBuyPrice;
            item.rare = ItemRarityID.Pink;
            item.EasySetLocalTextNameOverride("BrimstoneSwordEcType");
        }

        public override bool? AltFunctionUse(Item item, Player player) {
            return false;
        }

        public override bool? CanUseItem(Item item, Player player) {
            return player.altFunctionUse != 2;
        }

        public override bool? On_UseItem(Item item, Player player) {
            return false;
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            type = ModContent.ProjectileType<BrimstoneSwordHeldProj>();
        }

        public override void UseAnimation(Item item, Player player) {
            base.UseAnimation(item, player);
        }

        public override bool? On_UseAnimation(Item item, Player player) {
            return false;
        }
    }
}
