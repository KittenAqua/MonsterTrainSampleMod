using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enums.MTCardPools;
using MonsterTrainModdingAPI.Enums.MTClans;
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainTestMod.SamplePatches
{
    class NotHornBreakDataCreator
    {
        public static void RegisterCard()
        {
            CardDataBuilder cardDataBuilder = new CardDataBuilder
            {
                CardID = "TestMod_NotHornBreak",
                Name = "Not Horn Break",
                Description = "Deal [effect0.power] damage",
                Cost = 2,
                TargetsRoom = true,
                Targetless = false,
                ClanID = MTClanIDs.GetIDForType(typeof(MTClan_Awoken)),
                CardPoolIDs = new List<string> { MTCardPoolIDs.GetIDForType(typeof(MTCardPool_MegaPool)) },
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectDamage",
                        ParamInt = 12,
                        TargetMode = TargetMode.DropTargetCharacter
                    }
                },
                TraitBuilders = new List<CardTraitDataBuilder>
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateName = "CardTraitIgnoreArmor"
                    }
                }
            };

            cardDataBuilder.CreateAndSetCardArtPrefabVariantRef(
                "Assets/GameData/CardArt/Portrait_Prefabs/CardArt_Spell_FlashFreeze.prefab",
                "52471f4f40ea12d4a9a80a91f211fd07"
            );

            cardDataBuilder.BuildAndRegister();
        }
    }
}
