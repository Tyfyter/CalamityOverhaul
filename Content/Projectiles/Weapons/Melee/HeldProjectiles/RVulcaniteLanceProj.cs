﻿using CalamityMod;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Typeless;
using CalamityOverhaul.Content.Items.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.VulcaniteProj;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles
{
    internal class RVulcaniteLanceProj : BaseSpearProjectile
    {
        public override string Texture => CWRConstant.Cay_Proj_Melee + "Spears/VulcaniteLanceProj";
        public override LocalizedText DisplayName => CWRUtils.SafeGetItemName<VulcaniteLanceEcType>();
        public override void SetDefaults() {
            Projectile.width = Projectile.height = 44;
            Projectile.DamageType = ModContent.GetInstance<TrueMeleeDamageClass>();
            Projectile.timeLeft = 90;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }
        public override float InitialSpeed => 3f;
        public override float ReelbackSpeed => 2.4f;
        public override float ForwardSpeed => 0.95f;
        public override void ExtraBehavior() {
            if (Main.rand.NextBool(5))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Main.rand.NextBool(3) ? 16 : 127, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

            Vector2 goreVec = Projectile.Center + Projectile.velocity;
            if (Main.rand.NextBool(8) && Main.netMode != NetmodeID.Server) {
                int smoke = Gore.NewGore(Projectile.GetSource_FromAI(), goreVec, default, Main.rand.Next(375, 378), 1f);
                Main.gore[smoke].behindTiles = true;
            }

            foreach (Projectile proj in Main.projectile) {
                if (proj.type != ModContent.ProjectileType<VulcaniteBall>()) {
                    continue;
                }

                if (Projectile.Hitbox.Intersects(proj.Hitbox)) {
                    proj.Kill();

                    if (Projectile.owner == Main.myPlayer) {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), proj.Center, Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.1f, 0.1f)), ModContent.ProjectileType<TinyFlare>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        int boom = Projectile.NewProjectile(Projectile.GetSource_FromThis(), proj.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
                        if (boom.WithinBounds(Main.maxProjectiles))
                            Main.projectile[boom].DamageType = DamageClass.Melee;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            OnHitEffects(target.Center, hit.Crit);
            target.AddBuff(BuffID.OnFire3, 240);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            OnHitEffects(target.Center, true);
            target.AddBuff(BuffID.OnFire3, 240);
        }

        private void OnHitEffects(Vector2 targetPos, bool crit) {
            if (Projectile.owner == Main.myPlayer) {
                int boom = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
                if (boom.WithinBounds(Main.maxProjectiles))
                    Main.projectile[boom].DamageType = DamageClass.Melee;
            }
            if (crit) {
                var source = Projectile.GetSource_FromThis();
                for (int i = 0; i < 2; i++) {
                    if (Projectile.owner == Main.myPlayer) {
                        CalamityUtils.ProjectileBarrage(source, Projectile.Center, targetPos, Main.rand.NextBool(), 800f, 800f, 0f, 800f, 10f, ModContent.ProjectileType<TinyFlare>(), (int)(Projectile.damage * 0.5), 2f, Projectile.owner, true);
                    }
                }
            }
        }
    }
}
