using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace ShipRudderPatch
{
    [HarmonyPatch(typeof(Ship), "FixedUpdate")]
    public static class ApplyFixedUpdate
    {
        private static void Prefix(ref Ship __instance, ref List<Player> ___m_players, ref float ___m_backwardForce)
        {
            if (__instance.HaveControllingPlayer())
            {
                Ship.Speed speed = __instance.GetSpeedSetting();
                // only when padding backwards/forwards
                if (speed == Ship.Speed.Back || speed == Ship.Speed.Slow)
                {
                    int rowersCount = 0;

                    // we don't want to count controlling player
                    ZDOID controllingPlayer = (ZDOID) __instance.m_shipControlls.GetType().GetMethod("GetUser", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance.m_shipControlls, new object[] { });

                    foreach (Player player in ___m_players)
                    {
                        if (player.IsSitting() && !player.GetZDOID().Equals(controllingPlayer))
                        {
                            rowersCount++;
                        }
                    }
                    if (___m_backwardForce != 0.5f)  // we don't want to boost raft since it doesn't have any sitting spots
                    {
                        ___m_backwardForce = 0.2f + (rowersCount * 0.05f);
                    }

                }
            }
        }
    }
}
