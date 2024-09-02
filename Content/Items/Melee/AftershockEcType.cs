﻿using CalamityMod.Items;
using CalamityMod.Items.Weapons.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.EarthenProj;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Items.Melee
{
    /// <summary>
    /// 地炎阔刃
    /// </summary>
    internal class AftershockEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Melee + "Aftershock";
        public override void SetStaticDefaults() => ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        public override bool AltFunctionUse(Player player) => true;
        public override void SetDefaults() => SetDefaultsFunc(Item);
        internal static void SetDefaultsFunc(Item Item) {
            Item.damage = 65;
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 58;
            Item.useTime = 28;
            Item.useAnimation = 25;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7.5f;
            Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MeleeFossilShard>();
            Item.shootSpeed = 12f;
            Item.SetKnifeHeld<AftershockHeld>();
            if (Item.type == ModContent.ItemType<Aftershock>()) {
                Item.EasySetLocalTextNameOverride("AftershockEcType");
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
            => ShootFunc(player, source, position, velocity, type, damage, knockback);
        public static bool ShootFunc(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            player.GetItem().GiveMeleeType();
            if (player.altFunctionUse == 2) {
                player.GetItem().GiveMeleeType(true);
                Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 1.25f), knockback, player.whoAmI, 1);
                return false;
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}
