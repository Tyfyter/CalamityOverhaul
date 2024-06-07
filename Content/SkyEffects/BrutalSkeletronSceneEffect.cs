﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul.Content.SkyEffects
{
    internal class BrutalSkeletronSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot("CalamityOverhaul/Assets/Sounds/Music/TheDeadSpiritIsDead");
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player) => false;
    }
}
