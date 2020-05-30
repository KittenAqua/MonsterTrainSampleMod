using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainTestMod.SamplePatches
{
    [HarmonyPatch(typeof(CompendiumSectionCards), "IsCardUnlockedAndDiscovered")]
    class ShowAllCompendiumCardsPatch
    {
        static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}
