using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Enums;
using Trainworks.Constants;
using MonsterTrainModding;
using MonsterTrainTestMod.Clans;

namespace MonsterTrainTestMod.SamplePatches
{
    class Wimpcicle
    {
        public static readonly string ID = TestPlugin.GUID + "_WimpcicleRelic";

        public static void BuildAndRegister()
        {
            var cardPool = new CardPoolBuilder
            {
                CardPoolID = TestPlugin.GUID + "_TrainStewardCardPool",
                CardIDs = new List<string>
                {
                    VanillaCardIDs.TrainSteward
                }
            }.BuildAndRegister();

            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                Name = "Wimp-cicle",
                Description = "At the start of your turn, add a Train Steward to your hand",
                RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "assets/wimpcicle.png",
                ClanID = TestClan.ID,
                EffectBuilders = new List<RelicEffectDataBuilder>
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassName = "RelicEffectAddBattleCardToHand",
                        ParamInt = 1,
                        ParamCardPool = cardPool,
                        ParamTrigger = CharacterTriggerData.Trigger.PreCombat,
                        ParamTargetMode = TargetMode.FrontInRoom
                    }
                },
                Rarity = CollectableRarity.Common
            }.BuildAndRegister();
        }
    }
}
