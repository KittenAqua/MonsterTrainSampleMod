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
        // Creates a 2-cost, 12 damage Awoken spell named "Not Horn Break" with Flash Freeze's art
        static void Postfix(ref SaveManager __instance)
        {
            AllGameData allGameData = __instance.GetAllGameData();
            NotHornBreakDataCreator.CreateCardData(allGameData);
        }
    }

    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class AddToStartingDeck2
    {
        // Creates a 0-cost 3/4 with Train Steward's card art
        static void Postfix(ref SaveManager __instance)
        {
            __instance.AddCardToDeck(CustomCardManager.GetCardDataByID("NotHornBreak"));
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
                TargetsRoom = true,
                Targetless = false
            };

            cardDataBuilder.CreateAndSetCardArtPrefabVariantRef(
                "Assets/GameData/CardArt/Portrait_Prefabs/CardArt_Spell_FlashFreeze.prefab",
                "52471f4f40ea12d4a9a80a91f211fd07"
            );
            cardDataBuilder.SetCardClan(MTClan.Awoken);
            cardDataBuilder.AddToCardPool(MTCardPool.StandardPool);

            var damageEffectBuilder = new CardEffectDataBuilder
            {
                EffectStateName = "CardEffectDamage",
                ParamInt = 12,
                TargetMode = TargetMode.DropTargetCharacter
            };
            cardDataBuilder.Effects.Add(damageEffectBuilder.Build());

            var piercingTrait = new CardTraitDataBuilder
            {
                TraitStateName = "CardTraitIgnoreArmor"
            }.Build();
            cardDataBuilder.Traits.Add(piercingTrait);

            var customDescTrait = new CardTraitDataBuilder
            {
                TraitStateName = "CardTraitCustomDescription",
                ParamStr = "<size=50%><br><br></size>Deal [effect0.power] damage"
            }.Build();
            cardDataBuilder.Traits.Add(customDescTrait);

            cardDataBuilder.BuildAndRegister();
        }
    }
}
