using HarmonyLib;

namespace CompanyCruiserConfig.Patches
{
    [HarmonyPatch(typeof(VehicleController))]
    public class VehicleControllerPatch
    {
        [HarmonyPatch("PushTruckWithArms")]
        [HarmonyPrefix]
        private static void PushTruckWithForcePatch(VehicleController __instance)
        {
            __instance.pushForceMultiplier = CompanyCruiserConfig.pushForce.Value;
        }

        [HarmonyPatch("PushTruckClientRpc")]
        [HarmonyPrefix]
        private static void PushTruckClientRpcPatch(VehicleController __instance)
        {
            __instance.pushForceMultiplier = CompanyCruiserConfig.pushForce.Value;
        }
    }
}
