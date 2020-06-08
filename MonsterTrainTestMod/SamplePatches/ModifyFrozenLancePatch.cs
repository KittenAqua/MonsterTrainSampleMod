using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;

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

                // Remove the override description key, so we can replace the card's description with our own
                prop = typeof(CardData).GetField("overrideDescriptionKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(setCardData, "");

                // Set a custom card description
                var customDescTrait = new CardTraitDataBuilder
                {
                    TraitStateName = "CardTraitCustomDescription",
                    ParamStr = "<size=50%><br><br></size>Deal [effect0.power] damage to the front enemy unit and apply [frostbite] [effect1.status0.power]"
                }.Build();
                setCardData.GetTraits().Add(customDescTrait);

                // Set Frozen Lance's damage to 4967
                prop = typeof(CardEffectData).GetField("paramInt", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop.SetValue(setCardData.GetEffects()[0], 4967);
            }
        }
    }
}
