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

    class NotHornBreakDataCreator
    {
        public static void CreateCardData(AllGameData allGameData)
        {
            CardDataBuilder cardDataBuilder = new CardDataBuilder
            {
                CardID = "NotHornBreak",
                Name = "Not Horn Break",
                Cost = 2,
                OverrideDescriptionKey = "CardData_overrideDescriptionKey-4486d0ea967ad410-705ea064154a2624a8e7af1aabc85bb1-v2",
                TargetsRoom = true,
                Targetless = false
            };
            cardDataBuilder.CreateAndSetCardArtPrefabVariantRef(
                "Assets/GameData/CardArt/Portrait_Prefabs/CardArt_Spell_FlashFreeze.prefab",
                "52471f4f40ea12d4a9a80a91f211fd07"
            );
            cardDataBuilder.SetCardClan(MTClan.Awoken, allGameData);
            cardDataBuilder.AddToCardPool(MTCardPool.StandardPool);

            var damageEffectBuilder = new CardEffectDataBuilder
            {
                EffectStateName = "CardEffectDamage",
                ParamInt = 12,
                TargetMode = TargetMode.DropTargetCharacter
            };
            cardDataBuilder.Effects.Add(damageEffectBuilder.Build());

            var frostbiteEffectBuilder = new CardEffectDataBuilder
            {
                EffectStateName = "CardEffectAddStatusEffect",
                TargetMode = TargetMode.LastTargetedCharacters
            };
            frostbiteEffectBuilder.AddStatusEffect(MTStatusEffect.Poison, 14);
            cardDataBuilder.Effects.Add(frostbiteEffectBuilder.Build());

            cardDataBuilder.BuildAndRegister();
        }
    }
}
