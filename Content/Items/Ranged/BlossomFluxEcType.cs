﻿using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Projectiles.Ranged;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class BlossomFluxEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "BlossomFlux";
        public override void SetDefaults() => SetDefaultsFunc(Item);
        public static void SetDefaultsFunc(Item Item) {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 38;
            Item.height = 68;
            Item.useTime = 4;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0.15f;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<LeafArrow>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
            Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
            Item.rare = ItemRarityID.Lime;
            Item.Calamity().canFirePointBlankShots = true;
            Item.CWR().heldProjType = ModContent.ProjectileType<BlossomFluxHeldProj>();
            Item.CWR().hasHeldNoCanUseBool = true;
        }
    }
}
