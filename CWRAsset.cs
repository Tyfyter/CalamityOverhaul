﻿using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace CalamityOverhaul
{
    internal class CWRAsset : ICWRLoader
    {
        public static Asset<Texture2D> icon_small;
        public static Asset<Texture2D> Dusts_SoulFire;
        public static Asset<Texture2D> IceParcloseAsset;
        public static Asset<Texture2D> Quiver_back_Asset;
        public static Asset<Texture2D> IceGod_back_Asset;
        void ICWRLoader.LoadAsset() {
            icon_small = CWRUtils.GetT2DAsset("CalamityOverhaul/icon_small");
            Dusts_SoulFire = CWRUtils.GetT2DAsset(CWRConstant.Dust + "SoulFire");
            IceParcloseAsset = CWRUtils.GetT2DAsset(CWRConstant.Projectile + "IceParclose");
            Quiver_back_Asset = CWRUtils.GetT2DAsset(CWRConstant.Asset + "Players/Quiver_back");
            IceGod_back_Asset = CWRUtils.GetT2DAsset(CWRConstant.Asset + "Players/IceGod_back");
        }
        void ICWRLoader.UnLoadData() {
            icon_small = null;
            Dusts_SoulFire = null;
            IceParcloseAsset = null;
            Quiver_back_Asset = null;
            IceGod_back_Asset = null;
        }
    }
}
