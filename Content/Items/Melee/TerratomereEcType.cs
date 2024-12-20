﻿using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Healing;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.Core;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Items.Melee
{
    /// <summary>
    /// 泰拉巨刃
    /// </summary>
    internal class TerratomereEcType : EctypeItem
    {
        public static readonly SoundStyle SwingSound = new SoundStyle("CalamityMod/Sounds/Item/TerratomereSwing");
        public override string Texture => CWRConstant.Cay_Wap_Melee + "Terratomere";
        public override void SetDefaults() {
            Item.width = 60;
            Item.height = 66;
            Item.damage = 185;
            Item.DamageType = DamageClass.Melee;
            Item.useAnimation = 21;
            Item.useTime = 21;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.knockBack = 7f;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.UseSound = null;
            Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.SetKnifeHeld<TerratomereHeld>();
        }
    }

    internal class TerratomereHeld : BaseKnife
    {
        public override int TargetID => ModContent.ItemType<Terratomere>();
        public override string trailTexturePath => CWRConstant.Masking + "MotionTrail4";
        public override string gradientTexturePath => CWRConstant.ColorBar + "Swordsplosion_Bar";
        public override void SetKnifeProperty() {
            Projectile.width = Projectile.height = 122;
            overOffsetCachesRoting = MathHelper.ToRadians(-8);
            IgnoreImpactBoxSize = true;
            drawTrailHighlight = false;
            canDrawSlashTrail = true;
            Incandescence = true;
            drawTrailBtommWidth = 30;
            drawTrailTopWidth = 32;
            distanceToOwner = 40;
            OtherMeleeSize = 1f;
            unitOffsetDrawZkMode = 0;
            SwingData.baseSwingSpeed = 0;
            ShootSpeed = 20;
            Length = 54;
        }

        public override void Shoot() {
            int type = ModContent.ProjectileType<TerratomereSwordBeam>();
            for (int i = 0; i < 3; i++) {
                Projectile.NewProjectile(Source, Owner.Center, ShootVelocity.RotatedBy((-1 + i) * 0.1f), type
                , Projectile.damage / 2, Projectile.knockBack / 2, Owner.whoAmI);
            }
        }

        public override bool PreInOwnerUpdate() {
            ExecuteAdaptiveSwing(initialMeleeSize: 1, phase1Ratio: 0.3333f, phase2Ratio: 0.6f, phase0SwingSpeed: -1f
                , phase1SwingSpeed: 8f, phase2SwingSpeed: 3f, phase0MeleeSizeIncrement: 0.012f
                , phase2MeleeSizeIncrement: -0.01f, swingSound: new SoundStyle("CalamityMod/Sounds/Item/TerratomereSwing"));
            return base.PreInOwnerUpdate();
        }

        public override void KnifeHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(ModContent.BuffType<GlacialState>(), Terratomere.TrueMeleeGlacialStateTime);

            if (target.lifeMax > 5) {
                int heal = (int)Math.Round(Projectile.damage * 0.025);
                if (heal > 100)
                    heal = 100;

                if (Main.player[Main.myPlayer].lifeSteal <= 0f || heal <= 0)
                    return;

                CalamityGlobalProjectile.SpawnLifeStealProjectile(Projectile, Main.player[Projectile.owner]
                    , heal, ModContent.ProjectileType<ReaverHealOrb>(), 100);
            }

            int slashCreatorID = ModContent.ProjectileType<TerratomereSlashCreator>();
            if (Owner.ownedProjectileCounts[slashCreatorID] < 4) {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero
                    , slashCreatorID, Projectile.damage, Projectile.knockBack, Projectile.owner
                    , target.whoAmI, Main.rand.NextFloat(MathHelper.TwoPi));
                Owner.ownedProjectileCounts[slashCreatorID]++;
            }
        }
    }
}
