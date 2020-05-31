using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builder;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enum;

namespace MonsterTrainTestMod.SamplePatches
{
    [HarmonyPatch(typeof(SaveManager), "Initialize")]
    class RegisterNotHornBreak
    {
        // Creates a 2-cost, 12 damage spell named "Not Horn Break" with Flash Freeze's art
        static void Postfix(ref SaveManager __instance)
        {
            AllGameData allGameData = __instance.GetAllGameData();
            NotHornBreakDataCreator.CreateCardData(allGameData);
        }
    }

    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class AddNotHornBreakToStartingDeck
    {
        // Adds Not Horn Break to the player's starting deck if the primary class is Awoken.
        static void Postfix(ref SaveManager __instance)
        {
            if (CustomCardManager.CurrentPrimaryClan().GetTitle() == "Awoken")
            {
                var notHornBreakCardData = CustomCardManager.GetCardDataByID("NotHornBreak");
                __instance.AddCardToDeck(notHornBreakCardData, null, false, false, false, true, true);
            }
        }
    }

    class NotHornBreakDataCreator
    {
        public static void CreateCardData(AllGameData allGameData)
        {
            CardDataBuilder cardDataBuilder = new CardDataBuilder
            {
                CardID = "NotHornBreak",
                Name = "Not Horn Break",
                Cost = 2,
                OverrideDescriptionKey = "CardData_overrideDescriptionKey-cea726817805edf9-cb7f8f1805d4cd4449185ec893355baa-v2",
                TargetsRoom = true,
                Targetless = false
            };
            cardDataBuilder.CreateAndSetCardArtPrefabVariantRef(
                "Assets/GameData/CardArt/Portrait_Prefabs/CardArt_Spell_FlashFreeze.prefab",
                "52471f4f40ea12d4a9a80a91f211fd07"
            );
            cardDataBuilder.SetCardClan(MTClan.Awoken, allGameData);

            var damageEffect = new CardEffectData();
            AccessTools.Field(typeof(CardEffectData), "effectStateName")
                .SetValue(damageEffect, "CardEffectDamage");
            AccessTools.Field(typeof(CardEffectData), "paramInt")
                .SetValue(damageEffect, 12);
            AccessTools.Field(typeof(CardEffectData), "targetMode")
                .SetValue(damageEffect, TargetMode.DropTargetCharacter);
            AccessTools.Field(typeof(CardEffectData), "targetModeStatusEffectsFilter")
                .SetValue(damageEffect, new String[0]);
            cardDataBuilder.Effects.Add(damageEffect);

            cardDataBuilder.BuildAndRegister();
        }
    }
}
