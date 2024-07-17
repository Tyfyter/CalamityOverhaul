﻿using CalamityMod;
using CalamityMod.Projectiles.Melee;
using CalamityOverhaul.Content.Items.Melee.Extras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles
{
    internal class GuardianTerraHeld : BaseHeldProj
    {
        public override string Texture => CWRConstant.Item_Melee + "GuardianTerra";
        public float[] oldrot = new float[7];
        private Vector2 startVector;
        private Vector2 vector;
        public ref float Length => ref Projectile.localAI[0];
        public ref float Rot => ref Projectile.localAI[1];
        public float Timer;
        private float speed;
        private float SwingSpeed;
        private float glow;
        public override void SetStaticDefaults() {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool ShouldUpdatePosition() => false;
        public override void SetDefaults() {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.width = 46;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Rot = MathHelper.ToRadians(2);
            Length = 152;
        }

        public float SetSwingSpeed(float speed) => speed / Owner.GetAttackSpeed(DamageClass.Melee);

        public static Vector2 PolarVector(float radius, float theta) => new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center
                , Rot.ToRotationVector2() * Length * Projectile.scale * 1.1f + Owner.Center, 88, ref point);
        }

        public void InOnwer() {
            if (Projectile.ai[0] == 0) {
                if (Timer++ == 0) {
                    speed = MathHelper.ToRadians(1);
                    startVector = PolarVector(1, Projectile.velocity.ToRotation() - ((MathHelper.PiOver2 + 0.6f) * Projectile.spriteDirection));
                    vector = startVector * Length;
                    SoundEngine.PlaySound(SoundID.Item71, Owner.position);
                }
                if (Timer < 6 * SwingSpeed) {
                    Rot += speed / SwingSpeed * Projectile.spriteDirection;
                    speed += 0.15f;
                    vector = startVector.RotatedBy(Rot) * Length;
                }
                else {
                    Rot += speed / SwingSpeed * Projectile.spriteDirection;
                    speed *= 0.7f;
                    vector = startVector.RotatedBy(Rot) * Length;
                }
                if (Timer >= 25 * SwingSpeed) {
                    Projectile.Kill();
                }
            }
            else if (Projectile.ai[0] == 1) {
                if (Timer++ == 0) {
                    speed = MathHelper.ToRadians(1);
                    Projectile.velocity = PolarVector(5, (Main.MouseWorld - Owner.Center).ToRotation());
                    startVector = PolarVector(1, (Main.MouseWorld - Owner.Center).ToRotation() + ((MathHelper.PiOver2 + 0.6f) * Owner.direction));
                    vector = startVector * Length;
                    SoundEngine.PlaySound(SoundID.Item71, Projectile.position);
                }
                if (Timer < 6 * SwingSpeed) {
                    Rot -= speed / SwingSpeed * Projectile.spriteDirection;
                    speed += 0.15f;
                    vector = startVector.RotatedBy(Rot) * Length;
                }
                else {
                    Rot -= speed / SwingSpeed * Projectile.spriteDirection;
                    speed *= 0.7f;
                    vector = startVector.RotatedBy(Rot) * Length;
                }
                if (Timer >= 25 * SwingSpeed) {
                    Projectile.Kill();
                }
            }
        }

        public override void AI() {
            if (Owner.noItems || Owner.CCed || Owner.dead || !Owner.active) {
                Projectile.Kill();
            }
            SetHeld();
            SwingSpeed = SetSwingSpeed(1.2f);
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;

            Projectile.spriteDirection = Owner.direction;
            if (Projectile.ai[0] < 2) {
                if (Projectile.spriteDirection == 1)
                    Projectile.rotation = (Projectile.Center - Owner.Center).ToRotation() + MathHelper.PiOver4;
                else
                    Projectile.rotation = (Projectile.Center - Owner.Center).ToRotation() - MathHelper.Pi - MathHelper.PiOver4;
                glow += 0.03f;
            }

            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Owner.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);
            if (Projectile.IsOwnedByLocalPlayer()) {
                InOnwer();
            }
            if (Timer > 1) {
                Projectile.alpha = 0;
            }
            Projectile.scale = 1.5f;
            Projectile.Center = Owner.GetPlayerStabilityCenter() + vector;
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--) {
                oldrot[k] = oldrot[k - 1];
            }
            oldrot[0] = Projectile.rotation;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            int type = ModContent.ProjectileType<TerratomereSlashCreator>();
            if (Owner.ActiveItem().type == ModContent.ItemType<GuardianTerra>() && Owner.ownedProjectileCounts[type] < 1) {
                int proj = Projectile.NewProjectile(new EntitySource_ItemUse(Owner, Owner.ActiveItem()), Projectile.Center, Vector2.Zero
                    , type, Projectile.damage, 0, Projectile.owner, target.whoAmI, Main.rand.NextFloat(MathHelper.TwoPi));
                Main.projectile[proj].timeLeft = 130;
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new(texture.Width / 2f, texture.Height / 2f);
            Vector2 trialOrigin = new(texture.Width / 2f - 36, Projectile.height / 2f);
            int shader = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;
            Vector2 tovmgs = PolarVector(20, (Projectile.Center - Owner.Center).ToRotation());

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None
                , RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            GameShaders.Armor.ApplySecondary(shader, Owner, null);

            for (int k = 0; k < Projectile.oldPos.Length; k++) {
                Vector2 drawPos = Projectile.oldPos[k] - tovmgs - Main.screenPosition + trialOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.LimeGreen * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color * Projectile.Opacity * glow, oldrot[k], origin, Projectile.scale, spriteEffects, 0);
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None
                , RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            Main.EntitySpriteDraw(texture, Projectile.Center - tovmgs - Main.screenPosition + Vector2.UnitY * Projectile.gfxOffY, null
                , Projectile.GetAlpha(CWRUtils.MultiStepColorLerp(0.6f, Color.White, lightColor)), Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
