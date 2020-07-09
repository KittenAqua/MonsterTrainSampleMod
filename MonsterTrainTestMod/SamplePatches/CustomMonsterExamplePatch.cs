using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Constants;
using System.Linq;
using UnityEngine;

namespace MonsterTrainTestMod.SamplePatches
{
    [HarmonyPatch(typeof(SaveManager), "SetupRun")]
    class AddToStartingDeck
    {
        // Creates a 0-cost 3/4 with Train Steward's card art
        static void Postfix(ref SaveManager __instance)
        {
            __instance.AddCardToDeck(CustomCardManager.GetCardDataByID("TestMod_BlueEyes"));
        }
    }

    class BlueEyesDataCreator
    {
        public static void RegisterCard()
        {
            new CardDataBuilder
            {
                CardID = "TestMod_BlueEyes",
                Name = "Blue-Eyes White Dragon",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "MonsterTrainTestMod/blueeyes.png",
                ClanID = "TestMod_TestClan",
                CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner },
                EffectBuilders = new List<CardEffectDataBuilder>
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateName = "CardEffectSpawnMonster",
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = new CharacterDataBuilder
                        {
                            CharacterID = "TestMod_Character_BlueEyes",
                            Name = "Blue-Eyes White Dragon",
                            Size = 5,
                            Health = 2500,
                            AttackDamage = 3000,
                            AssetPath = "MonsterTrainTestMod/blueeyes_character.png"
                        }
                    }
                },
                EffectTriggerBuilders = new List<CharacterTriggerDataBuilder>
                {
                    new CharacterTriggerDataBuilder
                    {
                        Trigger = CharacterTriggerData.Trigger.OnAttacking,
                        EffectBuilders = new List<CardEffectDataBuilder>
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateName = "CardEffectAddStatusEffect",
                                TargetMode = TargetMode.Self
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}
