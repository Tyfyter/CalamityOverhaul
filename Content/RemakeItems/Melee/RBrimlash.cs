﻿using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using CalamityOverhaul.Content.Items.Melee;
using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Melee
{
    internal class RBrimlash : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<Brimlash>();
        public override int ProtogenesisID => ModContent.ItemType<BrimlashEcType>();
        public override void SetStaticDefaults() {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[TargetID] = true;
        }
        public override string TargetToolTipItemName => "BrimlashEcType";

        public override bool? AltFunctionUse(Item item, Player player) => true;

        public override void SetDefaults(Item item) {
            item.width = item.height = 72;
            item.damage = 70;
            item.DamageType = DamageClass.Melee;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.Swing;
            item.knockBack = 6f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.value = CalamityGlobalItem.RarityLimeBuyPrice;
            item.rare = ItemRarityID.Lime;
            item.shoot = ModContent.ProjectileType<BrimlashProj>();
            item.shootSpeed = 10f;
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
            item.useTime = item.useAnimation = 30;
            item.noUseGraphic = item.noMelee = false;
            if (player.altFunctionUse == 2) {
                item.useTime = item.useAnimation = 22;
                item.noUseGraphic = item.noMelee = true;
            }
        }

        public override bool? Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            if (player.altFunctionUse == 2) {
                SoundEngine.PlaySound(SoundID.Item84, position);
                Lighting.AddLight(position, Color.Red.ToVector3());

                if (Main.rand.NextBool(16))
                    player.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 20);

                float randRot = Main.rand.NextFloat(MathHelper.TwoPi);
                for (int i = 0; i < 5; i++) {
                    Vector2 vr = (MathHelper.TwoPi / 5f * i + randRot).ToRotationVector2() * 15;
                    Projectile.NewProjectile(source, position, vr, ModContent.ProjectileType<Brimlash2>(), damage, knockback, player.whoAmI);
                }

                return false;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
