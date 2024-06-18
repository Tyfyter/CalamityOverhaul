﻿using CalamityOverhaul.Content.NPCs.OverhaulBehavior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.NPCs.Core
{
    /// <summary>
    /// 所有关于NPC行为覆盖和性质加载的钩子在此处挂载
    /// </summary>
    internal class NPCSystem : ModSystem
    {
        internal delegate void On_OnHitByProjectileDelegate(NPC npc, Projectile projectile, in NPC.HitInfo hit, int damageDone);
        internal delegate void On_ModifyIncomingHitDelegate(NPC npc, ref NPC.HitModifiers modifiers);
        internal delegate bool On_NPCDelegate(NPC npc);
        internal delegate bool On_DrawDelegate(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor);
        internal delegate void On_DrawDelegate2(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor);

        public static Type npcLoaderType;
        public static List<NPCCoverage> NPCSets { get; private set; }
        public static MethodInfo onHitByProjectile_Method;
        public static MethodInfo modifyIncomingHit_Method;
        public static MethodInfo onPreAI_Method;
        public static MethodInfo onPreDraw_Method;
        public static MethodInfo onPostDraw_Method;

        public override void PostSetupContent() {
            //加载生物定义
            new PerforatorBehavior().Load();
            new HiveMindBehavior().Load();
        }

        private void LoadNPCSets() {
            NPCSets = new List<NPCCoverage>();
            foreach (Type type in CWRUtils.GetSubclasses(typeof(NPCCoverage))) {
                if (type != typeof(NPCCoverage)) {
                    object obj = Activator.CreateInstance(type);
                    if (obj is NPCCoverage inds) {
                        if (inds.CanLoad()) {
                            NPCSets.Add(inds);
                        }
                    }
                }
            }
        }

        MethodInfo getMethodInfo(string key) => npcLoaderType.GetMethod(key, BindingFlags.Public | BindingFlags.Static);

        void LoaderMethodAndHook() {
            {
                onHitByProjectile_Method = getMethodInfo("OnHitByProjectile");
                if (onHitByProjectile_Method != null) {
                    MonoModHooks.Add(onHitByProjectile_Method, OnHitByProjectileHook);
                }
            }
            {
                modifyIncomingHit_Method = getMethodInfo("ModifyIncomingHit");
                if (modifyIncomingHit_Method != null) {
                    MonoModHooks.Add(modifyIncomingHit_Method, ModifyIncomingHitHook);
                }
            }
            {
                onPreAI_Method = getMethodInfo("PreAI");
                if (onPreAI_Method != null) {
                    MonoModHooks.Add(onPreAI_Method, OnPreAIHook);
                }
            }
            {
                onPreDraw_Method = getMethodInfo("PreDraw");
                if (onPreDraw_Method != null) {
                    MonoModHooks.Add(onPreDraw_Method, OnPreDrawHook);
                }
            }
            {
                onPostDraw_Method = getMethodInfo("PostDraw");
                if (onPostDraw_Method != null) {
                    MonoModHooks.Add(onPostDraw_Method, OnPostDrawHook);
                }
            }
        }

        public override void Load() {
            npcLoaderType = typeof(NPCLoader);
            LoadNPCSets();
            LoaderMethodAndHook();
        }

        public static bool OnPreAIHook(On_NPCDelegate orig, NPC npc) {
            foreach (var set in NPCSets) {
                if (npc.type == set.TargetID) {
                    bool? reset = set.AI(npc, CWRMod.Instance);
                    if (reset.HasValue) {
                        return reset.Value;
                    }
                }
            }
            return orig.Invoke(npc);
        }

        public static bool OnPreDrawHook(On_DrawDelegate orig, NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            foreach (var set in NPCSets) {
                if (npc.type == set.TargetID) {
                    bool? reset = set.Draw(CWRMod.Instance, npc, spriteBatch, screenPos, drawColor);
                    if (reset.HasValue) {
                        return reset.Value;
                    }
                }
            }
            return orig.Invoke(npc, spriteBatch, screenPos, drawColor);
        }

        public static void OnPostDrawHook(On_DrawDelegate2 orig, NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            foreach (var set in NPCSets) {
                if (npc.type == set.TargetID) {
                    bool reset = set.PostDraw(CWRMod.Instance, npc, spriteBatch, screenPos, drawColor);
                    if (!reset) {
                        return;
                    }
                }
            }
            orig.Invoke(npc, spriteBatch, screenPos, drawColor);
        }

        public void OnHitByProjectileHook(On_OnHitByProjectileDelegate orig, NPC npc, Projectile projectile, in NPC.HitInfo hit, int damageDone) {
            foreach (NPCCustomizer inds in CWRMod.NPCCustomizerInstances) {
                bool? shouldOverride = null;
                if (inds.On_OnHitByProjectile_IfSpan(projectile)) {
                    shouldOverride = inds.On_OnHitByProjectile(npc, projectile, hit, damageDone);
                }
                if (shouldOverride.HasValue) {
                    if (shouldOverride.Value) {
                        npc.ModNPC?.OnHitByProjectile(projectile, hit, damageDone);
                        return;
                    }
                    else {
                        return;
                    }
                }
            }
            orig.Invoke(npc, projectile, hit, damageDone);
        }

        public void ModifyIncomingHitHook(On_ModifyIncomingHitDelegate orig, NPC npc, ref NPC.HitModifiers modifiers) {
            foreach (NPCCustomizer inds in CWRMod.NPCCustomizerInstances) {
                bool? shouldOverride = inds.On_ModifyIncomingHit(npc, ref modifiers);
                if (shouldOverride.HasValue) {
                    if (shouldOverride.Value) {
                        npc.ModNPC?.ModifyIncomingHit(ref modifiers);
                        return;
                    }
                    else {
                        return;
                    }
                }
            }
            orig.Invoke(npc, ref modifiers);
        }
    }
}
