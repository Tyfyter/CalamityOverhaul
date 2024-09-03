﻿using CalamityMod;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Melee.HeldProjectiles
{
    internal class DarklightGreatswordHeld : BaseKnife
    {
        public override int TargetID => ModContent.ItemType<DarklightGreatsword>();
        public override string gradientTexturePath => CWRConstant.ColorBar + "DarklightGreatsword_Bar";
        public override void SetKnifeProperty() {
            canDrawSlashTrail = true;
            drawTrailTopWidth = 30;
            distanceToOwner = 40;
            drawTrailBtommWidth = 60;
            SwingData.baseSwingSpeed = 3.5f;
            Projectile.width = Projectile.height = 66;
            SwingAIType = SwingAITypeEnum.UpAndDown;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Length = 86;
        }

        public override void Shoot() {
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Owner.Center + UnitToMouseV * 42
                , UnitToMouseV * 7, ModContent.ProjectileType<DarkBeam>()
                , (int)(Projectile.damage * 0.8f), Projectile.knockBack * 0.8f, Owner.whoAmI);
        }

        public override void KnifeHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            target.AddBuff(Main.rand.NextBool() ? BuffID.Frostburn2 : BuffID.OnFire3, 240);
            int type = ModContent.ProjectileType<DarklightGreatswordSlashCreator>();
            int damg = (int)(Owner.CalcIntDamage<MeleeDamageClass>(Item.damage) * 0.8f);
            float kack = Item.knockBack * 0.9f;
            if (Owner.ownedProjectileCounts[type] < 3) {
                Projectile.NewProjectile(Owner.GetSource_ItemUse(Item), target.Center, Vector2.Zero, type
                    , damg, kack, Owner.whoAmI, target.whoAmI, Owner.itemRotation, Main.rand.Next(2));
                Owner.ownedProjectileCounts[type]++;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info) {
            target.AddBuff(Main.rand.NextBool() ? BuffID.Frostburn2 : BuffID.OnFire3, 240);
        }
    }
}
