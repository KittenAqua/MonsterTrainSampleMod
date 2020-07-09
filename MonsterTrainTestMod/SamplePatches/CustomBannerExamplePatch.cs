using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainTestMod.SamplePatches
{
    class AllUnitsBannerCreator
    {
        public static void RegisterBanner()
        {
            CardPool cardPool = UnityEngine.ScriptableObject.CreateInstance<CardPool>();
            var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(cardPool);
            cardDataList.Add(CustomCardManager.GetCardDataByID("NotHornBreak0"));
            cardDataList.Add(CustomCardManager.GetCardDataByID("NotHornBreak1"));
            cardDataList.Add(CustomCardManager.GetCardDataByID("NotHornBreak2"));
            cardDataList.Add(CustomCardManager.GetCardDataByID("NotHornBreak3"));
            cardDataList.Add(CustomCardManager.GetCardDataByID("NotHornBreak4"));
            cardDataList.Add(CustomCardManager.GetCardDataByID("NotHornBreak5"));

            //var assetLoadingData = (AssetLoadingData)AccessTools.Field(typeof(AssetLoadingManager), "_assetLoadingData").GetValue(AssetLoadingManager.GetInst());
            //var cardPools = assetLoadingData.CardPoolsAll;
            //CardPool cardPool = null;
            //foreach (var pool in cardPools)
            //{
            //    if (pool.name == "MegaPoolWithUnits")
            //    {
            //        cardPool = pool;
            //    }
            //}

            new RewardNodeDataBuilder()
            {
                RewardNodeID = "TestMod_MegaPoolWithUnitsBanner",
                MapNodePoolIDs = new List<string> { "RandomChosenMainClassUnit", "RandomChosenSubClassUnit" },
                Name = "Variety Banner",
                Description = "Offers both spells and units of either clan",
                RequiredClass = CustomClassManager.GetClassDataByID("TestMod_TestClan"),
                FrozenSpritePath = "MonsterTrainTestMod/AllCardsBanner_Frozen.png",
                EnabledSpritePath = "MonsterTrainTestMod/AllCardsBanner_Enabled.png",
                DisabledSpritePath = "MonsterTrainTestMod/AllCardsBanner_Disabled.png",
                DisabledVisitedSpritePath = "MonsterTrainTestMod/AllCardsBanner_Disabled_Visited.png",
                GlowSpritePath = "MonsterTrainTestMod/AllCardsBanner_Glow.png",
                MapIcon = CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/AllUnitsBanner.png"),
                MinimapIcon = CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/AllUnitsBanner.png"),
                SkipCheckInBattleMode = true,
                OverrideTooltipTitleBody = false,
                NodeSelectedSfxCue = "Node_Banner",
                RewardBuilders = new List<IRewardDataBuilder>
                {
                    new DraftRewardDataBuilder()
                    {
                        DraftRewardID = "TestMod_CardDraftMegaPoolWithUnits",
                        _RewardSprite = CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/AllUnitsBanner.png"),
                        _RewardTitleKey = "Variety reward!",
                        _RewardDescriptionKey = "Choose a card!",
                        Costs = new int[] { 100 },
                        _IsServiceMerchantReward = true,
                        DraftPool = cardPool,
                        ClassType = (RunState.ClassType)7,
                        DraftOptionsCount = 2,
                        RarityFloorOverride = CollectableRarity.Uncommon
                    }
                }
            }.BuildAndRegister();
        }
    }
}
