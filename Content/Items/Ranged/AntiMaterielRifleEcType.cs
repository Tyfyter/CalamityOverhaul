﻿using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Rarities;
using CalamityMod.Sounds;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Items.Ranged
{
    internal class AntiMaterielRifleEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Ranged + "AntiMaterielRifle";
        public override void SetDefaults() {
            Item.damage = 1060;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 154;
            Item.height = 40;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 9.5f;
            Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
            Item.UseSound = CommonCalamitySounds.LargeWeaponFireSound;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ModContent.RarityType<DarkBlue>();
            Item.Calamity().canFirePointBlankShots = true;
            Item.CWR().hasHeldNoCanUseBool = true;
            Item.CWR().heldProjType = ModContent.ProjectileType<AntiMaterielRifleHeldProj>();
            Item.CWR().HasCartridgeHolder = true;
            Item.CWR().AmmoCapacity = 6;
            Item.CWR().Scope = true;
        }
    }
}
