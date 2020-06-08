//using System;
//using System.Collections.Generic;
//using System.Text;
//using HarmonyLib;

//namespace MonsterTrainTestMod.SamplePatches
//{
//    // A poorly written patch to remove faction restriction from cards.
//    // When possible, you should avoid overwriting the old method like this for compatibility purposes.
//    [HarmonyPatch(typeof(CardPoolHelper), "GetCardsForClass")]
//    [HarmonyPatch(new Type[] { typeof(CardPool), typeof(ClassData), typeof(CollectableRarity), typeof(CardPoolHelper.RarityCondition), typeof(bool) })]
//    class NoFactionRestrictionPatch
//    {
//        static void Postfix(ref List<CardData> __result, ref CardPool cardPool, ClassData classData, CollectableRarity paramRarity, CardPoolHelper.RarityCondition rarityCondition = null, bool testRarityCondition = true)
//        {
//            List<CardData> list = new List<CardData>();
//            for (int i = 0; i < cardPool.GetNumCards(); i++)
//            {
//                CardData cardAtIndex = cardPool.GetCardAtIndex(i);
//                if ((!testRarityCondition || rarityCondition(paramRarity, cardAtIndex.GetRarity())))
//                {
//                    list.Add(cardAtIndex);
//                }
//            }
//            __result = list;
//        }
//    }
//}
