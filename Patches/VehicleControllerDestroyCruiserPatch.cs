using CompanyCruiserConfig.Utils;
using HarmonyLib;

namespace CompanyCruiserConfig.Patches
{
    [HarmonyPatch(typeof(VehicleController))]
    internal class VehicleControllerDestroyCruiserPatch
    {
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        private static void FixedUpdatePostfix(VehicleController __instance)
        {
            CompanyCruiserConfig.Logger.LogDebug("patch worked ok");
            if (__instance.transform.position.y < CompanyCruiserConfig.despawnHeightThreshold)
            {
                __instance.OnDisable();
                __instance.gameObject.AddComponent<DestroyObject>();
            }
        }
    }
}
