using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using HarmonyLib;
using System;

namespace CorpseInfo
{
    [StaticConstructorOnStartup]
    public static class CorpseInfoMod
    {
        public static Harmony harmonyInstance;

        static CorpseInfoMod()
        {
            harmonyInstance = new Harmony("arl85.CorpseInfo");
            harmonyInstance.PatchAll();
        }

        [HarmonyPatch(typeof(Corpse), "SpecialDisplayStats")]
        public static class Corpse_SpecialDisplayStats
        {
            static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> values, Corpse __instance)
            {
                if (__instance.InnerPawn != null)
                {
                    foreach (StatDrawEntry value in values) { yield return value; }
                    yield return new StatDrawEntry(StatCategoryDefOf.PawnMisc, "CorpseInfo.CorpseOf".Translate(), __instance.InnerPawn.LabelNoCountColored.CapitalizeFirst(), "CorpseInfo.CorpseOfDescription".Translate(), 9999, null, new Dialog_InfoCard.Hyperlink[] { new Dialog_InfoCard.Hyperlink(__instance.InnerPawn) });
                }
            }
        }

        [HarmonyPatch(typeof(StatPart_Food), "TransformValue")]
        public static class StatPart_Food_TransformValue
        {
            static bool Prefix(StatRequest req)
            {
                if (req.HasThing && req.Thing is Pawn pawn && pawn.Dead)
                {
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(StatPart_Food), "ExplanationPart")]
        public static class StatPart_Food_ExplanationPart
        {
            static bool Prefix(StatRequest req, string __result)
            {
                if (req.HasThing && req.Thing is Pawn pawn && pawn.Dead)
                {
                    __result = null;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(StatPart_Rest), "TransformValue")]
        public static class StatPart_Rest_TransformValue
        {
            static bool Prefix(StatRequest req)
            {
                if (req.HasThing && req.Thing is Pawn pawn && pawn.Dead)
                {
                    return false;
                }
                return true;
            }

        }

        [HarmonyPatch(typeof(StatPart_Rest), "ExplanationPart")]
        public static class StatPart_Rest_ExplanationPart
        {
            static bool Prefix(StatRequest req, string __result)
            {
                if (req.HasThing && req.Thing is Pawn pawn && pawn.Dead)
                {
                    __result = null;
                    return false;
                }
                return true;
            }

        }
    }
}

