﻿using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Sounds;
using CalamityOverhaul.Content.Items.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles;
using CalamityOverhaul.Content.RemakeItems.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Melee
{
    internal class RDiseasedPike : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<CalamityMod.Items.Weapons.Melee.DiseasedPike>();
        public override int ProtogenesisID => ModContent.ItemType<DiseasedPikeEcType>();
        public override void SetStaticDefaults() {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[TargetID] = true;
        }
        public override string TargetToolTipItemName => "DiseasedPikeEcType";

        public override void SetDefaults(Item item) {
            item.width = 62;
            item.damage = 65;
            item.DamageType = ModContent.GetInstance<TrueMeleeDamageClass>();
            item.noMelee = true;
            item.useTurn = true;
            item.noUseGraphic = true;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.Shoot;
            item.useTime = 20;
            item.knockBack = 8.5f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.height = 58;
            item.value = CalamityGlobalItem.RarityYellowBuyPrice;
            item.rare = ItemRarityID.Yellow;
            item.shoot = ModContent.ProjectileType<RDiseasedPikeSpear>();
            item.shootSpeed = 10f;
        }

        public override bool? AltFunctionUse(Item item, Player player) => true;

        public override bool? CanUseItem(Item item, Player player) => player.ownedProjectileCounts[item.shoot] <= 0;

        public override bool? Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            if (player.altFunctionUse == 2) {
                SoundEngine.PlaySound(in CommonCalamitySounds.MeatySlashSound, player.Center);
                item.CWR().MeleeCharge = 0;
                Main.projectile[proj].ai[1] = 1;
            }
            return false;
        }
    }
}
