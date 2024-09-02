﻿using CalamityMod;
using CalamityMod.Items;
using CalamityOverhaul.Content.Items.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles;
using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Melee
{
    internal class RAstralPike : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AstralPike>();
        public override int ProtogenesisID => ModContent.ItemType<AstralPikeEcType>();
        public override string TargetToolTipItemName => "AstralPikeEcType";


        public override void SetDefaults(Item item) {
            item.width = 44;
            item.damage = 90;
            item.DamageType = DamageClass.Melee;
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 13;
            item.useStyle = ItemUseStyleID.Shoot;
            item.useTime = 13;
            item.knockBack = 8.5f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 50;
            item.value = CalamityGlobalItem.RarityCyanBuyPrice;
            item.rare = ItemRarityID.Cyan;
            item.shoot = ModContent.ProjectileType<RAstralPikeProj>();
            item.shootSpeed = 13f;
        }
    }
}
