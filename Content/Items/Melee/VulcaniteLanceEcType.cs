﻿using CalamityMod;
using CalamityMod.Items;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.VulcaniteProj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Items.Melee
{
    /// <summary>
    /// 火山长矛
    /// </summary>
    internal class VulcaniteLanceEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Melee + "VulcaniteLance";
        public override void SetStaticDefaults() => ItemID.Sets.Spears[Item.type] = true;
        public override void SetDefaults() {
            Item.width = 44;
            Item.damage = 90;
            Item.DamageType = ModContent.GetInstance<TrueMeleeDamageClass>();
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 22;
            Item.knockBack = 6.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 44;
            Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<RVulcaniteLanceProj>();
            Item.shootSpeed = 10f;

        }

        public override void HoldItem(Player player) {
            if (Main.rand.NextBool(13)) {
                Vector2 pos = player.Center + new Vector2(Main.rand.Next(-320, 230), Main.rand.Next(-160, 32));
                Projectile.NewProjectile(player.parent(), pos, new Vector2(0, -1), ModContent.ProjectileType<VulcaniteBall>(), Item.damage, 0, player.whoAmI);
            }
        }

        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] <= 0;
    }
}
