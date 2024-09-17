﻿using CalamityOverhaul.Content.UIs.Core;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria;

namespace CalamityOverhaul.Content.UIs
{
    internal class ILMainMenuModification : ILoader
    {
        internal static List<BaseMainMenuOverUI> MainMenuOverUIInstances;

        public static void HanderLoadBaseMenuOverUIType() {
            MainMenuOverUIInstances = [];
            List<Type> rItemIndsTypes = CWRUtils.GetSubclasses(typeof(BaseMainMenuOverUI));
            foreach (Type type in rItemIndsTypes) {
                if (type != typeof(BaseMainMenuOverUI)) {
                    object obj = Activator.CreateInstance(type);
                    if (obj is BaseMainMenuOverUI inds) {
                        if (inds.CanLoad()) {
                            inds.Load();
                            MainMenuOverUIInstances.Add(inds);
                        }
                    }
                }
            }
        }

        public static void ILMenuLoadDrawFunc(ILContext il) {
            ILCursor potlevel = new(il);

            if (!potlevel.TryGotoNext(
                i => i.MatchLdsfld(typeof(Main), nameof(Main.spriteBatch)),
                i => i.Match(OpCodes.Ldc_I4_0),
                i => i.MatchLdsfld(typeof(BlendState), nameof(BlendState.AlphaBlend)),
                i => i.MatchLdsfld(typeof(Main), nameof(Main.SamplerStateForCursor)),
                i => i.MatchLdsfld(typeof(DepthStencilState), nameof(DepthStencilState.None)),
                i => i.MatchLdsfld(typeof(RasterizerState), nameof(RasterizerState.CullCounterClockwise)),
                i => i.Match(OpCodes.Ldnull),
                i => i.MatchCall(typeof(Main), $"get_{nameof(Main.UIScaleMatrix)}")
            )) {
                string conxt2 = CWRUtils.Translation("IL 挂载失败，是否是目标流已经更改或者移除框架?"
                    , "IL mount failed. Has the target stream changed or the frame has been removed?");
                string errortext = $"{nameof(ILMainMenuModification)}: {conxt2} ";
                CWRMod.Instance.Logger.Info(errortext);
                throw new Exception(errortext);
            }

            _ = potlevel.EmitDelegate(() => Draw(Main.spriteBatch));
        }

        void ILoader.Load() {
            if (Main.dedServ) {
                return;
            }
            // 该类是客户端内容，因此，不要在服务器上调用这个加载函数
            HanderLoadBaseMenuOverUIType();
            IL_Main.DrawMenu += ILMenuLoadDrawFunc;
        }

        void ILoader.UnLoad() {
            if (Main.dedServ) {
                return;
            }
            // 该类是客户端内容，因此，不要在服务器上调用这个卸载函数
            foreach (BaseMainMenuOverUI baseMainMenuOverUI in MainMenuOverUIInstances) {
                baseMainMenuOverUI.UnLoad();
            }
            IL_Main.DrawMenu -= ILMenuLoadDrawFunc;
            MainMenuOverUIInstances = null;
        }

        private static void Draw(SpriteBatch sb) {
            if (Main.gameMenu && MainMenuOverUIInstances != null && MainMenuOverUIInstances.Count > 0) {
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp
                    , DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
                foreach (BaseMainMenuOverUI baseMainMenuOverUI in MainMenuOverUIInstances) {
                    if (!baseMainMenuOverUI.Active) {
                        continue;
                    }
                    baseMainMenuOverUI.Update(Main.gameTimeCache);
                    baseMainMenuOverUI.Draw(sb);
                }
                sb.End();
            }
        }
    }
}
