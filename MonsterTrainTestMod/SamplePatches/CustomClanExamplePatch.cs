using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Constants;
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainTestMod.SamplePatches
{
    class TestClanDataCreator
    {
        public static void RegisterClan()
        {
            var copyClan = CustomClassManager.SaveManager.GetAllGameData().GetAllClassDatas()[0];
            new ClassDataBuilder
            {
                ClassID = "TestMod_TestClan",
                Name = "Test Clan",
                Description = "Test Clan Description",
                SubclassDescription = "Test Clan Sub Description",
                CardStyle = ClassCardStyle.Umbra,
                ChampionIcon = CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/testclan_large.png"),
                ClanSelectSfxCue = copyClan.GetClanSelectSfxCue(),
                ClassUnlockCondition = MetagameSaveData.TrackedValue.None,
                ClassUnlockParam = 0,
                Icons = new List<Sprite>
                {
                    CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/testclan_small.png"),
                    CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/testclan_large.png"),
                    CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/testclan_large.png"),
                    CustomAssetManager.LoadSpriteFromPath("MonsterTrainTestMod/testclan_silhouette.png")
                },
                StartingChampion = copyClan.GetStartingChampionData(),
                UiColor = new Color(0.43f, 0.15f, 0.81f, 1f),
                UiColorDark = new Color(0.12f, 0.42f, 0.39f, 1f),
                UpgradeTree = copyClan.GetUpgradeTree()
            }.BuildAndRegister();
        }
    }
}
