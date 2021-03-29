using System;
using BepInEx;
using HarmonyLib;

namespace RowingVikings
{
    [BepInPlugin("gaijinx.mod.rowing_vikings", "RowingVikings", "0.1")]
    public class RowingVikingssPlugin : BaseUnityPlugin
    {
        void Awake()
        {
                UnityEngine.Debug.Log("Installing Gaijin mod");
                var harmony = new Harmony("gaijinx.mod.rowing_vikings");
                harmony.PatchAll();
        }
    }
}
