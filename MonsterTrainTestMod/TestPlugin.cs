using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI;
using MonsterTrainModdingAPI.Interfaces;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainTestMod.SamplePatches;
using System.IO;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin("com.name.package.generic", "Test Plugin", "0.1")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    [BepInDependency("api.modding.train.monster")]
    public class TestPlugin : BaseUnityPlugin, IInitializable
    {
        public void Initialize()
        {
            var harmony = new Harmony("com.name.package.generic");
            harmony.PatchAll();
            TestClanDataCreator.RegisterClan();
            NotHornBreakDataCreator.RegisterCard("NotHornBreak0", CollectableRarity.Starter);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak1", CollectableRarity.Common);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak2", CollectableRarity.Common);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak3", CollectableRarity.Common);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak4", CollectableRarity.Uncommon);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak5", CollectableRarity.Uncommon);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak6", CollectableRarity.Uncommon);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak7", CollectableRarity.Rare);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak8", CollectableRarity.Rare);
            NotHornBreakDataCreator.RegisterCard("NotHornBreak9", CollectableRarity.Rare);
            BlueEyesDataCreator.RegisterCard();
            WimpcicleDataCreator.RegisterRelic();
            AllUnitsBannerCreator.RegisterBanner();
        }
    }
}
