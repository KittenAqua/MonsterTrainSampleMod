using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainTestMod.SamplePatches
{
    [HarmonyPatch(typeof(CardState), "Setup")]
    class ModifyFrozenLancePatch
    {
        // Static boolean to ensure the code triggers only once, courtesy of Nyoxide
        private static bool triggered;

        static void Prefix(ref CardData setCardData)
        {
            if (!triggered && setCardData.GetName() == "Frozen Lance")
            {
                triggered = true;

                // Add piercing to Frozen Lance's trait list
                var piercingTrait = new CardTraitData();
                piercingTrait.Setup("CardTraitIgnoreArmor");
                setCardData.GetTraits().Add(piercingTrait);

                // Instantiate a new card effect. We're going to use it to add 327 frostbite to Frozen Lance.
                var frostbiteEffect = new CardEffectData();

                // Tell the game that this card effect should add a status effect to the target(s)
                var prop = typeof(CardEffectData).GetField("effectStateName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(frostbiteEffect, "CardEffectAddStatusEffect");

                // Set targeting mode to be the same one used by Flash Freeze: Last Targeted Characters
                prop = typeof(CardEffectData).GetField("targetMode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(frostbiteEffect, TargetMode.LastTargetedCharacters);

                // This field can't be null, or the game crashes with a NullPointerException
                // Seems like all the other null fields are fine, though, at least at a glance
                prop = typeof(CardEffectData).GetField("targetModeStatusEffectsFilter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(frostbiteEffect, new String[0]);

                // Create the Frostbite status and add it to the effect's status array
                StatusEffectStackData frostbiteStatus = new StatusEffectStackData();
                frostbiteStatus.statusId = "poison";
                frostbiteStatus.count = 327;
                prop = typeof(CardEffectData).GetField("paramStatusEffects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(frostbiteEffect, new StatusEffectStackData[] { frostbiteStatus });

                // Add the Frostbite effect to Frozen Lance's card effect list
                setCardData.GetEffects().Add(frostbiteEffect);

                // Add Frostbite to Frozen Lance's description by copying the description override from Flash Freeze
                prop = typeof(CardData).GetField("overrideDescriptionKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(setCardData, "CardData_overrideDescriptionKey-4486d0ea967ad410-705ea064154a2624a8e7af1aabc85bb1-v2");

                // Set Frozen Lance's damage to 2
                prop = typeof(CardEffectData).GetField("paramInt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(setCardData.GetEffects()[0], 4967);
            }
        }
    }
}
