using CalamityOverhaul.Common;
using CalamityOverhaul.Common.Effects;
using CalamityOverhaul.Content;
using CalamityOverhaul.Content.Events;
using CalamityOverhaul.Content.Items;
using CalamityOverhaul.Content.NPCs.Core;
using CalamityOverhaul.Content.OthermodMROs.Thorium.Core;
using CalamityOverhaul.Content.Particles.Core;
using CalamityOverhaul.Content.RemakeItems.Core;
using CalamityOverhaul.Content.Structures;
using CalamityOverhaul.Content.UIs;
using CalamityOverhaul.Content.UIs.SupertableUIs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace CalamityOverhaul
{
    public class CWRMod : Mod
    {
        #region Date
        internal static CWRMod Instance;
        internal static int GameLoadCount;

        internal Mod musicMod = null;
        internal Mod betterWaveSkipper = null;
        internal Mod fargowiltasSouls = null;
        internal Mod catalystMod = null;
        internal Mod weaponOut = null;
        internal Mod weaponDisplay = null;
        internal Mod magicBuilder = null;
        internal Mod improveGame = null;
        internal Mod luiafk = null;
        internal Mod terrariaOverhaul = null;
        internal Mod thoriumMod = null;
        internal Mod narakuEye = null;
        internal Mod coolerItemVisualEffect = null;

        internal static bool Suitableversion_improveGame { get; private set; }

        internal List<Mod> LoadMods { get; private set; }
        internal List<ISetupData> SetupDatas { get; private set; }
        internal static List<BaseRItem> RItemInstances { get; private set; } = new List<BaseRItem>();
        internal static List<EctypeItem> EctypeItemInstance { get; private set; } = new List<EctypeItem>();
        internal static List<NPCCustomizer> NPCCustomizerInstances { get; private set; } = new List<NPCCustomizer>();
        internal static Dictionary<int, BaseRItem> RItemIndsDict { get; private set; } = new Dictionary<int, BaseRItem>();
        internal static GlobalHookList<GlobalItem> CWR_InItemLoader_Set_Shoot_Hook { get; private set; }
        internal static GlobalHookList<GlobalItem> CWR_InItemLoader_Set_CanUse_Hook { get; private set; }
        internal static GlobalHookList<GlobalItem> CWR_InItemLoader_Set_UseItem_Hook { get; private set; }

        internal enum CallType
        {
            SupertableRecipeDate,
        }
        #endregion

        public override object Call(params object[] args) {
            CallType callType = (CallType)args[0];
            if (callType == CallType.SupertableRecipeDate) {
                return SupertableUI.RpsDataStringArrays;
            }
            return null;
        }

        public override void PostSetupContent() {
            LoadMods = ModLoader.Mods.ToList();
            //����ģ��Call��Ҫ���м���
            FromThorium.PostLoadData();

            {
                RItemInstances = new List<BaseRItem>();//����ֱ�ӽ��г�ʼ�����㲻����Ҫ����UnLoadж��
                List<Type> rItemIndsTypes = CWRUtils.GetSubclasses(typeof(BaseRItem));
                //($"һ����ȡ��{rItemIndsTypes.Count}������ѡԪ��Type").DompInConsole();
                foreach (Type type in rItemIndsTypes) {
                    //($"ָ��Ԫ��{type}���з���").DompInConsole();
                    if (type != typeof(BaseRItem)) {
                        object obj = Activator.CreateInstance(type);
                        if (obj is BaseRItem inds) {
                            //($"Ԫ��{type}�ɹ�ת��Ϊobject�����з���").DompInConsole();
                            if (inds.CanLoad()) {
                                //($"���ڳ�ʼ��Ԫ��{type}").DompInConsole();
                                inds.Load();
                                inds.SetStaticDefaults();
                                if (inds.TargetID != 0) {
                                    //($"�ɹ�����Ԫ��{type}").DompInConsole();
                                    //("______________________________").DompInConsole();
                                    RItemInstances.Add(inds);
                                }//������ж�һ��TargetID�Ƿ�Ϊ0����Ϊ�������һ����Ч��Ritemʵ������ô����TargetID�Ͳ�����Ϊ0����������ӽ�ȥ�ᵼ��LoadRecipe���ֱ���
                                else {
                                    //($"Ԫ��{type}��TargetID����0������ʧ��").DompInConsole();
                                }
                            }
                            else {
                                //($"Ԫ��{type}CanLoad����false").DompInConsole();
                            }
                        }
                        else {
                            //($"Ԫ��{type}ת��BaseRItemʧ��").DompInConsole();
                        }
                    }
                    else {
                        //($"Ԫ��{type}��{typeof(BaseRItem)}").DompInConsole();
                    }
                }
                //($"{RItemInstances.Count}��Ԫ���Ѿ�װ�ؽ�RItemInstances").DompInConsole();
            }

            {
                EctypeItemInstance = new List<EctypeItem>();
                List<Type> ectypeIndsTypes = CWRUtils.GetSubclasses(typeof(BaseRItem));
                foreach (Type type in ectypeIndsTypes) {
                    if (type != typeof(EctypeItem)) {
                        object obj = Activator.CreateInstance(type);
                        if (obj is EctypeItem inds) {
                            EctypeItemInstance.Add(inds);
                        }
                    }
                }
            }

            {
                NPCCustomizerInstances = new List<NPCCustomizer>();//����ֱ�ӽ��г�ʼ�����㲻����Ҫ����UnLoadж��
                List<Type> npcCustomizerIndsTypes = CWRUtils.GetSubclasses(typeof(NPCCustomizer));
                foreach (Type type in npcCustomizerIndsTypes) {
                    if (type != typeof(NPCCustomizer)) {
                        object obj = Activator.CreateInstance(type);
                        if (obj is NPCCustomizer inds) {
                            NPCCustomizerInstances.Add(inds);
                        }
                    }
                }
            }

            {
                RItemIndsDict = new Dictionary<int, BaseRItem>();
                foreach (BaseRItem ritem in RItemInstances) {
                    RItemIndsDict.Add(ritem.SetReadonlyTargetID, ritem);
                }
                ($"{RItemIndsDict.Count}�������Ѿ�װ�ؽ�RItemIndsDict").DompInConsole();
            }

            {
                GlobalHookList<GlobalItem> getItemLoaderHookTargetValue(string key)
                    => (GlobalHookList<GlobalItem>)typeof(ItemLoader).GetField(key, BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
                CWR_InItemLoader_Set_Shoot_Hook = getItemLoaderHookTargetValue("HookShoot");
                CWR_InItemLoader_Set_CanUse_Hook = getItemLoaderHookTargetValue("HookCanUseItem");
                CWR_InItemLoader_Set_UseItem_Hook = getItemLoaderHookTargetValue("HookUseItem");
            }

            {
                Suitableversion_improveGame = false;
                if (improveGame != null) {
                    Suitableversion_improveGame = improveGame.Version >= new Version(1, 7, 1, 7);
                }
            }

            //����һ��ID�б���������ؿ��Ա������������Ѿ���Ӻ���
            CWRLoad.Load();
            foreach (var i in SetupDatas) { 
                i.SetupData();
                if (!Main.dedServ) {
                    i.LoadAsset();
                }
            }
        }

        public override void Load() {
            Instance = this;
            SetupDatas = CWRUtils.GetSubInterface<ISetupData>("ISetupData");
            foreach (var setup in SetupDatas) {
                setup.LoadData();
            }
            FindMod();
            FromThorium.LoadData();
            ModGanged.Load();
            CWRParticleHandler.Load();
            new InWorldBossPhase().Load();
            CWRKeySystem.LoadKeyDate(this);
            StructuresBehavior.Load();
            On_Main.DrawInfernoRings += PeSystem.CWRDrawForegroundParticles;
            LoadClient();
            GameLoadCount++;
            base.Load();
        }

        public override void Unload() {
            FromThorium.UnLoadData();
            ModGanged.UnLoad();
            CWRParticleHandler.Unload();
            InWorldBossPhase.UnLoad();
            CWRKeySystem.Unload();
            StructuresBehavior.UnLoad();
            On_Main.DrawInfernoRings -= PeSystem.CWRDrawForegroundParticles;
            UnLoadClient();
            foreach (var setup in SetupDatas) {
                setup.UnLoadData();
            }
            SetupDatas = null;
            base.Unload();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) => CWRNetCode.HandlePacket(this, reader, whoAmI);

        private void emptyMod() {
            musicMod = null;
            betterWaveSkipper = null;
            fargowiltasSouls = null;
            catalystMod = null;
            weaponOut = null;
            weaponDisplay = null;
            magicBuilder = null;
            improveGame = null;
            luiafk = null;
            terrariaOverhaul = null;
            thoriumMod = null;
            narakuEye = null;
            coolerItemVisualEffect = null;
        }

        public void FindMod() {
            emptyMod();
            ModLoader.TryGetMod("CalamityModMusic", out musicMod);
            ModLoader.TryGetMod("BetterWaveSkipper", out betterWaveSkipper);
            ModLoader.TryGetMod("FargowiltasSouls", out fargowiltasSouls);
            ModLoader.TryGetMod("CatalystMod", out catalystMod);
            ModLoader.TryGetMod("WeaponOut", out weaponOut);
            ModLoader.TryGetMod("WeaponDisplay", out weaponDisplay);
            ModLoader.TryGetMod("MagicBuilder", out magicBuilder);
            ModLoader.TryGetMod("ImproveGame", out improveGame);
            ModLoader.TryGetMod("miningcracks_take_on_luiafk", out luiafk);
            ModLoader.TryGetMod("TerrariaOverhaul", out terrariaOverhaul);
            ModLoader.TryGetMod("ThoriumMod", out thoriumMod);
            ModLoader.TryGetMod("NarakuEye", out narakuEye);
            ModLoader.TryGetMod("CoolerItemVisualEffect", out coolerItemVisualEffect);
        }

        public void LoadClient() {
            if (Main.dedServ)
                return;
            
            EffectsRegistry.LoadEffects();
            ILMainMenuModification.Load();
            Filters.Scene["CWRMod:TungstenSky"] = new Filter(new TungstenSkyDate("FilterMiniTower").UseColor(0.5f, 0f, 0.5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["CWRMod:TungstenSky"] = new TungstenSky();
        }

        public void UnLoadClient() {
            if (Main.dedServ)
                return;

            EffectsRegistry.UnLoad();
            ILMainMenuModification.Unload();
        }
    }
}