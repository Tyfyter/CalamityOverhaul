﻿using CalamityOverhaul.Content.RemakeItems.Core;
using Terraria.ID;
using Terraria;
using CalamityOverhaul.Content.Projectiles.Weapons.Melee.Core;

namespace CalamityOverhaul.Content.RemakeItems.Vanilla
{
    /// <summary>
    /// 冰雪镰刀
    /// </summary>
    internal class RIceSickle : BaseRItem
    {
        public override int TargetID => ItemID.IceSickle;
        public override bool IsVanilla => true;
        public override void SetDefaults(Item item) {
            item.UseSound = null;
            item.SetKnifeHeld<IceSickleHeld>();
        }
    }

    internal class IceSickleHeld : BaseKnife
    {
        public override int TargetID => ItemID.IceSickle;
        public override string gradientTexturePath => CWRConstant.ColorBar + "BrinyBaron_Bar";
        public override void SetKnifeProperty() {
            drawTrailHighlight = false;
            canDrawSlashTrail = true;
            drawTrailCount = 10;
            drawTrailTopWidth = 46;
            distanceToOwner = -22;
            drawTrailBtommWidth = 0;
            SwingData.baseSwingSpeed = 4f;
            Projectile.width = Projectile.height = 46;
            Length = 60;
            shootSengs = 0.8f;
            unitOffsetDrawZkMode = -2;
        }

        public override bool PreInOwnerUpdate() {
            ExecuteAdaptiveSwing(initialMeleeSize: 1, phase0SwingSpeed: -0.3f
                , phase1SwingSpeed: 5.2f, phase2SwingSpeed: 3f, swingSound: SoundID.Item71);
            return base.PreInOwnerUpdate();
        }

        public override void Shoot() {
            Projectile.NewProjectile(Source, ShootSpanPos, ShootVelocity, ProjectileID.IceSickle, Projectile.damage, 2);
        }
    }
}
