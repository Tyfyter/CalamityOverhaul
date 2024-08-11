﻿using CalamityOverhaul.Content;
using CalamityOverhaul.Content.Events;
using CalamityOverhaul.Content.NPCs.BrutalNPCs.BrutalMechanicalEye;
using CalamityOverhaul.Content.NPCs.BrutalNPCs.BrutalSkeletronPrime;
using CalamityOverhaul.Content.TileEntitys;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityOverhaul
{
    public enum CWRMessageType : byte
    {
        DompBool,
        RecoilAcceleration,
        TungstenRiot,
        TEBloodAltar,
        OverBeatBack,
        BrutalSkeletronPrimeAI,
        BrutalTwinsAI,
        ProjViscosityData,
    }

    public class CWRNetCode
    {
        public static void HandlePacket(Mod mod, BinaryReader reader, int whoAmI) {
            CWRMessageType type = (CWRMessageType)reader.ReadByte();
            if (type == CWRMessageType.DompBool) {
                Main.player[reader.ReadInt32()].CWR().HandleDomp(reader);
            }
            else if (type == CWRMessageType.RecoilAcceleration) {
                Main.player[reader.ReadInt32()].CWR().HandleRecoilAcceleration(reader);
            }
            else if (type == CWRMessageType.TungstenRiot) {
                TungstenRiot.Instance.TungstenRiotIsOngoing = reader.ReadBoolean();
                TungstenRiot.Instance.EventKillPoints = reader.ReadInt32();
            }
            else if (type == CWRMessageType.TEBloodAltar) {
                TEBloodAltar.ReadTEData(mod, reader);
            }
            else if (type == CWRMessageType.OverBeatBack) {
                byte npcIdx = reader.ReadByte();
                NPC npc = Main.npc[npcIdx];
                if (npc.type == NPCID.None || !npc.active) {
                    return;
                }
                CWRNpc modnpc = npc.CWR();
                modnpc.OverBeatBackBool = reader.ReadBoolean();
                modnpc.OverBeatBackVr = reader.ReadVector2();
                modnpc.OverBeatBackAttenuationForce = reader.ReadSingle();
            }
            else if (type == CWRMessageType.BrutalSkeletronPrimeAI) {
                BrutalSkeletronPrimeAI.NetAIReceive(reader);
            }
            else if (type == CWRMessageType.BrutalTwinsAI) {
                bool isSpazmatism = reader.ReadBoolean();
                if (isSpazmatism) {
                    for (int i = 0; i < SpazmatismAI.ai.Length; i++) {
                        SpazmatismAI.ai[i] = reader.ReadInt32();
                    }
                }
                else {
                    for (int i = 0; i < RetinazerAI.ai.Length; i++) {
                        RetinazerAI.ai[i] = reader.ReadInt32();
                    }
                }
            }
            else if (type == CWRMessageType.ProjViscosityData) {
                CWRProjectile.NetViscosityReceive(reader);
            }
        }
    }
}
