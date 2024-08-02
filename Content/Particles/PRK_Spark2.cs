﻿using CalamityOverhaul.Content.Particles.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using Terraria;

namespace CalamityOverhaul.Content.Particles
{
    internal class PRK_Spark2 : BaseParticle
    {
        public Color InitialColor;
        public bool AffectedByGravity;
        public override bool SetLifetime => true;
        public override bool UseCustomDraw => true;
        public override bool UseAdditiveBlend => false;
        public Entity entity;
        public override string Texture => "CalamityMod/Projectiles/StarProj";

        public PRK_Spark2(Vector2 relativePosition, Vector2 velocity, bool affectedByGravity, int lifetime, float scale, Color color) {
            Position = relativePosition;
            Velocity = velocity;
            AffectedByGravity = affectedByGravity;
            Scale = scale;
            Lifetime = lifetime;
            Color = InitialColor = color;
        }

        public override void AI() {
            Scale *= 0.95f;
            Color = Color.Lerp(InitialColor, Color.Transparent, (float)Math.Pow(LifetimeCompletion, 3D));
            Velocity *= 0.95f;
            if (Velocity.Length() < 12f && AffectedByGravity) {
                Velocity.X *= 0.94f;
                Velocity.Y += 0.25f;
            }
            Rotation = Velocity.ToRotation() + MathHelper.PiOver2;
            if (entity != null) {
                if (entity.active) {
                    Position += entity.velocity;
                }
            }
        }

        public override void CustomDraw(SpriteBatch spriteBatch) {
            Vector2 scale = new Vector2(0.5f, 1.6f) * Scale;
            Texture2D texture = DRKLoader.ParticleIDToTexturesDic[Type];

            spriteBatch.Draw(texture, Position - Main.screenPosition, null, Color, Rotation, texture.Size() * 0.5f, scale, 0, 0f);
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, Color, Rotation, texture.Size() * 0.5f, scale * new Vector2(0.45f, 1f), 0, 0f);
        }
    }
}
