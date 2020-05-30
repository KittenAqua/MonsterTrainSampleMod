using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builder;
using MonsterTrainModdingAPI.Enum;

namespace MonsterTrainTestMod.SamplePatches
{
    class NotHornBreakDataCreator
    {
        public static CardData CreateCardData(AllGameData allGameData)
        {
            CardDataBuilder cardDataBuilder = new CardDataBuilder();
            cardDataBuilder.Cost = 2;
            cardDataBuilder.Name = "Not Horn Break";
            cardDataBuilder.OverrideDescriptionKey = "CardData_overrideDescriptionKey-cea726817805edf9-cb7f8f1805d4cd4449185ec893355baa-v2";
            cardDataBuilder.TargetsRoom = true;
            cardDataBuilder.Targetless = false;
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

            CardData cardData = cardDataBuilder.BuildAndRegister(allGameData);

            allGameData.GetAllCardData().Add(cardData);

            return cardData;
        }
    }

    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class AddNotHornBreakToStartingDeck
    {
        // Adds a 2-cost, 12 damage spell named "Not Horn Break" with Flash Freeze's art to the player's starting deck if the primary class is Awoken.
        static void Postfix(ref SaveManager __instance, ref AllGameData ___allGameData)
        {
            CardData notHornBreakCardData = NotHornBreakDataCreator.CreateCardData(___allGameData);
            __instance.AddCardToDeck(notHornBreakCardData, null, false, false, false, true, true);
        }
    }
}
