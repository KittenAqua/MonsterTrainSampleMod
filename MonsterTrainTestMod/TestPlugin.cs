using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using ShinyShoe;

namespace MonsterTrainTestMod
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin("com.name.package.generic", "Test Plugin", "0.1")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class TestPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("com.name.package.generic");
            harmony.PatchAll();
        }
    }

    // Credit to Rawsome, Stable Infery for this one, too: the first modded code ever run in Monster Train.
    [HarmonyPatch(typeof(TooltipGenerator), nameof(TooltipGenerator.GetRelicTooltips))]
    class TooltipGenerator_GetRelicTooltips_Patch
    {
        static void Postfix(ref List<TooltipContent> tooltips)
        {
            tooltips.Add(new TooltipContent("Testi", "Moddy mod tooltip", TooltipDesigner.TooltipDesignType.Default));
        }
    }

    // Credit to Rawsome, Stable Infery for this one, too: a quick and dirty patch to disable the multiplayer button.
    [HarmonyPatch(typeof(MainMenuScreen), "CollectMenuButtons")]
    class MainMenuScreen_CollectMenuButtons_Patch
    {
        static void Postfix(ref GameUISelectableButton ___multiplayerButton, ref List<GameUISelectableButton> ___menuButtons)
        {
            ___menuButtons.Remove(___multiplayerButton);
            ___multiplayerButton.enabled = false;
        }
    }

    // A poorly written patch to remove faction restriction from cards.
    // When possible, you should avoid overwriting the old method like this for compatibility purposes.
    [HarmonyPatch(typeof(CardPoolHelper), "GetCardsForClass")]
    [HarmonyPatch(new Type[] { typeof(CardPool), typeof(ClassData), typeof(CollectableRarity), typeof(CardPoolHelper.RarityCondition), typeof(bool) })]
    class CardPoolHelper_GetCardsForClass_Patch
    {
        static void Postfix(ref List<CardData> __result, ref CardPool cardPool, ClassData classData, CollectableRarity paramRarity, CardPoolHelper.RarityCondition rarityCondition = null, bool testRarityCondition = true)
        {
            List<CardData> list = new List<CardData>();
            for (int i = 0; i < cardPool.GetNumCards(); i++)
            {
                CardData cardAtIndex = cardPool.GetCardAtIndex(i);
                if ((!testRarityCondition || rarityCondition(paramRarity, cardAtIndex.GetRarity())))
                {
                    list.Add(cardAtIndex);
                }
            }
            __result = list;
        }
    }
}
