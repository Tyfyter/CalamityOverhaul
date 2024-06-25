﻿using CalamityOverhaul.Common;
using CalamityOverhaul.Content.Projectiles.Weapons.Ranged.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace CalamityOverhaul.Content.Projectiles.Weapons.Ranged.HeldProjs.Vanilla
{
    internal class RevolverHeldProj : BaseFeederGun
    {
        public override string Texture => CWRConstant.Placeholder;
        public override Texture2D TextureValue => TextureAssets.Item[ItemID.Revolver].Value;
        public override int targetCayItem => ItemID.Revolver;
        public override int targetCWRItem => ItemID.Revolver;
        public override void SetRangedProperty() {
            kreloadMaxTime = 30;
            FireTime = 8;
            HandDistance = 18;
            HandDistanceY = 3;
            HandFireDistance = 18;
            HandFireDistanceY = -2;
            ShootPosNorlLengValue = -5;
            ShootPosToMouLengValue = 10;
            RepeatedCartridgeChange = true;
            CanCreateCaseEjection = false;
            GunPressure = 0.1f;
            ControlForce = 0.05f;
            Recoil = 0.9f;
            RangeOfStress = 25;
        }

        public override void PreInOwnerUpdate() {
            LoadingAnimation((int)(Time * 30 * DirSign), 3, 5);
        }

        public override void PostInOwnerUpdate() {
            if (kreloadTimeValue > 0) {
                ArmRotSengsFront = (MathHelper.PiOver2 * SafeGravDir - MathHelper.ToRadians(30)) * SafeGravDir + 0.3f;
                SetCompositeArm();
            }
        }

        public override void KreloadSoundloadTheRounds() {
            base.KreloadSoundloadTheRounds();
            for (int i = 0; i < 6; i++) {
                CaseEjection();
            }
        }
    }
}
