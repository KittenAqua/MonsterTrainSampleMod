using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builder;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enum;
using UnityEngine;

namespace MonsterTrainTestMod.SamplePatches
{
    [HarmonyPatch(typeof(SaveManager), "Initialize")]
    class RegisterTorchHead
    {
        // Creates a 0-cost 3/4 with Train Steward's card art
        static void Postfix(ref SaveManager __instance)
        {
            AllGameData allGameData = __instance.GetAllGameData();
            TorchHeadDataCreator.CreateCardData(allGameData);
        }
    }

    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class AddToStartingDeck
    {
        // Creates a 0-cost 3/4 with Train Steward's card art
        static void Postfix(ref SaveManager __instance)
        {
            __instance.AddCardToDeck(CustomCardManager.GetCardDataByID("TestMod_TorchHead"));
        }
    }

    class TorchHeadDataCreator
    {
        public static void CreateCardData(AllGameData allGameData)
        {
            CardDataBuilder cardDataBuilder = new CardDataBuilder
            {
                CardID = "TestMod_TorchHead",
                Name = "Torch Head",
                Cost = 0,
                OverrideDescriptionKey = "EmptyString-0000000000000000-00000000000000000000000000000000-v2",
                CardType = CardType.Monster,
                TargetsRoom = true,
                Targetless = false
            };
            cardDataBuilder.CreateAndSetCardArtPrefabVariantRef(
                "Assets/GameData/CardArt/Portrait_Prefabs/CardArt_TrainSteward.prefab",
                "a21c55c24d2e5d645a01230d874e26a9"
            );
            cardDataBuilder.SetCardClan(MTClan.Awoken);
            cardDataBuilder.AddToCardPool(MTCardPool.AwokenBannerPool);

            CharacterDataBuilder characterDataBuilder = new CharacterDataBuilder
            {
                CharacterID = cardDataBuilder.CardID,
                Name = cardDataBuilder.Name,
                Size = 1,
                Health = 4,
                AttackDamage = 3
            };
            characterDataBuilder.CreateAndSetCharacterArtPrefabVariantRef(
                "Assets/GameData/CharacterArt/Character_Prefabs/Character_TrainSteward.prefab",
                "8a96184904fce5745ab5139b620b4d31"
            );
            CharacterData characterData = characterDataBuilder.BuildAndRegister();

            var spawnEffectBuilder = new CardEffectDataBuilder
            {
                EffectStateName = "CardEffectSpawnMonster",
                TargetMode = TargetMode.DropTargetCharacter,
                ParamCharacterData = characterData
            };
            cardDataBuilder.Effects.Add(spawnEffectBuilder.Build());

            cardDataBuilder.BuildAndRegister();
        }
    }
}
