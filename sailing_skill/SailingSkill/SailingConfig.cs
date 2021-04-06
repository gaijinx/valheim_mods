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
        public int NEXUS_ID = 123123;

        private ConfigVariable<float> skillIncrease;
        private ConfigVariable<float> maxTailwindBoost;
        private ConfigVariable<float> maxForewindDampener;
        private ConfigVariable<float> maxDamageReduction;

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

        public void InitConfig(string id, ConfigFile config)
        {
            config.Bind<int>("General", "NexusID", 123123, "Nexus mod ID for updates");

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

            skillIncrease = new ConfigVariable<float>(assembly, config, id, "SkillIncrease", .05f, "Leveling", "Higher the value the faster it levels up", true);
            maxTailwindBoost = new ConfigVariable<float>(assembly, config, id, "maxTailwindBoost", .5f, "Limits", "Maximum tailwind boost", true);
            maxForewindDampener = new ConfigVariable<float>(assembly, config, id, "maxForewindDampener", .5f, "Limits", "Maximum forewind slowdown force dampener", true);
            maxDamageReduction = new ConfigVariable<float>(assembly, config, id, "maxDamageReduction", .5f, "Limits", "Maximum ship damage reduction", true);

        }
    }
}
