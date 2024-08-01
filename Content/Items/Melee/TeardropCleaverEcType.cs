﻿using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.Core;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityOverhaul.Content.Items.Melee
{
    /// <summary>
    /// 泪刃
    /// </summary>
    internal class TeardropCleaverEcType : EctypeItem
    {
        public override string Texture => CWRConstant.Cay_Wap_Melee + "TeardropCleaver";
        public override void SetDefaults() => SetDefaultsFunc(Item);
        public static void SetDefaultsFunc(Item Item) {
            Item.width = 56;
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 24;
            Item.useTurn = true;
            Item.knockBack = 5.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 66;
            Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<TeardropCleaverProj>();
            Item.shootSpeed = 1;
            Item.SetKnifeHeld<TeardropCleaverHeld>();
        }
    }

    internal class TeardropCleaverHeld : BaseKnife
    {
        public override int TargetID => ModContent.ItemType<TeardropCleaver>();
        public override string trailTexturePath => CWRConstant.Masking + "MotionTrail3";
        public override string gradientTexturePath => CWRConstant.ColorBar + "Greentide_Bar";
        public override void SetKnifeProperty() {
            Projectile.width = Projectile.height = 46;
            canDrawSlashTrail = true;
            distanceToOwner = 18;
            drawTrailBtommWidth = 50;
            drawTrailTopWidth = 20;
            drawTrailCount = 6;
            Length = 52;
            ShootSpeed = 2f;
        }

        public override void Shoot() {
            SoundEngine.PlaySound(SoundID.Item13, Owner.Center);
            Projectile.NewProjectile(Source, ShootSpanPos + ShootVelocity * 30, ShootVelocity
                , ModContent.ProjectileType<TeardropCleaverProj>(), Projectile.damage / 2
                , Projectile.knockBack, Owner.whoAmI);
        }

        public override bool PreInOwnerUpdate() {
            return base.PreInOwnerUpdate();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(ModContent.BuffType<TemporalSadness>(), 60);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            target.AddBuff(ModContent.BuffType<TemporalSadness>(), 60);
        }
    }
}
