using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace SailingSkill
{
    public class SailingConfig
    {
        public int NEXUS_ID = 922;

        private ConfigVariable<float> skillIncrease;
        private ConfigVariable<float> maxTailwindBoost;
        private ConfigVariable<float> maxForewindDampener;
        private ConfigVariable<float> maxDamageReduction;
        private ConfigVariable<int> skillIncreaseTick;
        private ConfigVariable<float> halfSailSkillIncreaseMultiplier;
        private ConfigVariable<float> fullSailSkillIncreaseMultiplier;

        public float SkillIncrease
        {
            get { return skillIncrease.Value; }
        }
        public float MaxTailwindBoost
        {
            get { return maxTailwindBoost.Value; }
        }
        public float MaxForewindDampener
        {
            get { return -maxForewindDampener.Value; }
        }
        public float MaxDamageReduction
        {
            get { return -maxDamageReduction.Value; }
        }

        public int SkillIncreaseTick
        {
            get { return skillIncreaseTick.Value; }
        }

        public float HalfSailSkillIncreaseMultiplier
        {
            get { return halfSailSkillIncreaseMultiplier.Value; }
        }

        public float FullSailSkillIncreaseMultiplier
        {
            get { return fullSailSkillIncreaseMultiplier.Value; }
        }

        public void InitConfig(string id, ConfigFile config)
        {
            config.Bind<int>("General", "NexusID", NEXUS_ID, "Nexus mod ID for updates");

            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "ModConfigEnforcer");

            if (assembly != null)
            {
                Debug.Log("[SailingSkill] Mod Config Enforcer detected, registering mod...");
                var configManagerType = assembly.GetType("ModConfigEnforcer.ConfigManager");
                Traverse.Create(configManagerType).Method("RegisterMod", id, config).GetValue(id, config);
            }
            else
            {
                Debug.Log("Mod Config Enforcer not detected.");
            }

            skillIncrease = new ConfigVariable<float>(assembly, config, id, "skillIncrease", .002f, "Leveling", "Amount of skill exp granted per skillIncreaseTick", true);
            skillIncreaseTick = new ConfigVariable<int>(assembly, config, id, "skillIncreaseTick", 120, "Leveling", "Number of boat update ticks to grant skill increase after, 60 is roughly equivalent to 1 second", true);
            halfSailSkillIncreaseMultiplier = new ConfigVariable<float>(assembly, config, id, "halfSailSkillIncreaseMultiplier", 1.5f, "Leveling", "Exp multiplier for half sail sailing speed", true);
            fullSailSkillIncreaseMultiplier = new ConfigVariable<float>(assembly, config, id, "fullSailSkillIncreaseMultiplier", 2.0f, "Leveling", "Exp multiplier for full sail sailing speed", true);

            maxTailwindBoost = new ConfigVariable<float>(assembly, config, id, "maxTailwindBoost", .5f, "Limits", "Maximum tailwind boost", true);
            maxForewindDampener = new ConfigVariable<float>(assembly, config, id, "maxForewindDampener", .5f, "Limits", "Maximum forewind slowdown force dampener", true);
            maxDamageReduction = new ConfigVariable<float>(assembly, config, id, "maxDamageReduction", .5f, "Limits", "Maximum ship damage reduction", true);
        }
    }
}
