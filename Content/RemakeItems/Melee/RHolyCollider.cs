﻿using CalamityMod.Items;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Rarities;
using CalamityOverhaul.Content.Items.Melee;
using CalamityOverhaul.Content.Particles;
using CalamityOverhaul.Content.Particles.Core;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee;
using CalamityOverhaul.Content.RemakeItems.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.RemakeItems.Melee
{
    internal class RHolyCollider : BaseRItem
    {
        public override int TargetID => ModContent.ItemType<HolyCollider>();
        public override int ProtogenesisID => ModContent.ItemType<HolyColliderEcType>();
        public override void Load() {
            SetReadonlyTargetID = TargetID;
        }
        public override void SetDefaults(Item item) {
            item.width = 94;
            item.height = 80;
            item.scale = 1f;
            item.damage = 230;
            item.DamageType = DamageClass.Melee;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.Swing;
            item.useTime = 16;
            item.useTurn = true;
            item.knockBack = 3.75f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<HolyColliderHolyFires>();
            item.shootSpeed = 10f;
            item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
            item.rare = ModContent.RarityType<Turquoise>();
            CWRUtils.EasySetLocalTextNameOverride(item, "HolyCollider");
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            CWRUtils.OnModifyTooltips(CWRMod.Instance, tooltips, "HolyCollider");
        }

        public override bool? Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            for (int i = 0; i < Main.rand.Next(3, 5); i++) {
                Projectile.NewProjectile(source, player.Center + Main.rand.NextVector2Unit() * Main.rand.Next(342, 468), velocity / 3, ModContent.ProjectileType<HolyColliderHolyFires>(), (int)(damage * 0.5f), knockback, player.whoAmI);
                for (int j = 0; j < 3; j++) {
                    Vector2 pos = player.Center + Main.rand.NextVector2Unit() * Main.rand.Next(342, 468);
                    Vector2 particleSpeed = pos.To(player.Center).UnitVector() * 7;
                    BaseParticle energyLeak = new DRK_HolyColliderLight(pos, particleSpeed
                        , Main.rand.NextFloat(0.5f, 0.7f), Color.Gold, 90, 1, 1.5f, hueShift: 0.0f);
                    DRKLoader.AddParticle(energyLeak);
                }
            }
            return false;
        }

        public override bool? On_OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone) {
            return false;
        }

        public override bool? On_OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo) {
            return false;
        }
    }
}
