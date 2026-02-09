using HarmonyLib;

namespace CompanyCruiserConfig.Patches
{
    [HarmonyPatch(typeof(VehicleController))]
    internal class VehicleControllerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        private static void StartPrefix(VehicleController __instance)
        {
            __instance.baseCarHP = CompanyCruiserConfig.baseCarHP;
        }

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartPostfix(VehicleController __instance) 
        {
            __instance.brakeSpeed = CompanyCruiserConfig.brakeSpeed;
            __instance.carAcceleration = CompanyCruiserConfig.carAcceleration;
            __instance.carFragility = CompanyCruiserConfig.carFragility;
            __instance.carHitPlayerForceFraction = CompanyCruiserConfig.carHitPlayerForceFraction;
            __instance.carMaxSpeed = CompanyCruiserConfig.carMaxSpeed;
            __instance.carReactToPlayerHitMultiplier = CompanyCruiserConfig.carReactToPlayerHitMultiplier;
            __instance.engineIntensityPercentage = CompanyCruiserConfig.engineIntensityPercentage;
            __instance.EngineTorque = CompanyCruiserConfig.engineTorque;
            __instance.idleSpeed = CompanyCruiserConfig.idleSpeed;
            __instance.jumpForce = CompanyCruiserConfig.jumpForce;
            __instance.MaxEngineRPM = CompanyCruiserConfig.maxEngineRPM;
            __instance.maximumBumpForce = CompanyCruiserConfig.maximumBumpForce;
            __instance.mediumBumpForce = CompanyCruiserConfig.mediumBumpForce;
            __instance.MinEngineRPM = CompanyCruiserConfig.minEngineRPM;
            __instance.minimalBumpForce = CompanyCruiserConfig.minimalBumpForce;
            __instance.movingAverageLength = CompanyCruiserConfig.movingAverageLength;
            __instance.pushForceMultiplier = CompanyCruiserConfig.pushForceMultiplier;
            __instance.pushVerticalOffsetAmount = CompanyCruiserConfig.pushVerticalOffsetAmount;
            __instance.radioSignalDecreaseThreshold = CompanyCruiserConfig.radioSignalDecreaseThreshold;
            __instance.radioSignalQuality = CompanyCruiserConfig.radioSignalQuality;
            __instance.radioSignalTurbulence = CompanyCruiserConfig.radioSignalTurbulence;
            __instance.speed = CompanyCruiserConfig.speed;
            __instance.springForce = CompanyCruiserConfig.springForce;
            __instance.stability = CompanyCruiserConfig.stability;
            __instance.steeringWheelTurnSpeed = CompanyCruiserConfig.steeringWheelTurnSpeed;
            __instance.syncRotationSpeed = CompanyCruiserConfig.syncRotationSpeed;
            __instance.syncSpeedMultiplier = CompanyCruiserConfig.syncSpeedMultiplier;
            __instance.torqueForce = CompanyCruiserConfig.torqueForce;
            __instance.turboBoostForce = CompanyCruiserConfig.turboBoostForce;
            __instance.turboBoostUpwardForce = CompanyCruiserConfig.turboBoostUpwardForce;

            if (CompanyCruiserConfig.editRigidbody)
            {
                __instance.mainRigidbody.angularDrag = CompanyCruiserConfig.angularDrag;
                __instance.mainRigidbody.drag = CompanyCruiserConfig.drag;
                __instance.mainRigidbody.mass = CompanyCruiserConfig.mass;
                __instance.mainRigidbody.maxAngularVelocity = CompanyCruiserConfig.maxAngularVelocity;
                __instance.mainRigidbody.maxDepenetrationVelocity = CompanyCruiserConfig.maxDepenetrationVelocity;
                __instance.mainRigidbody.maxLinearVelocity = CompanyCruiserConfig.maxLinearVelocity;
                __instance.mainRigidbody.sleepThreshold = CompanyCruiserConfig.sleepThreshold;
                __instance.mainRigidbody.solverIterations = CompanyCruiserConfig.solverIterations;
                __instance.mainRigidbody.solverVelocityIterations = CompanyCruiserConfig.solverVelocityIterations;
            }
        }

        [HarmonyPatch("PushTruckWithArms")]
        [HarmonyPrefix]
        private static void PushTruckWithForcePatch(VehicleController __instance)
        {
            __instance.pushForceMultiplier = CompanyCruiserConfig.pushForceMultiplier;
        }

        [HarmonyPatch("PushTruckClientRpc")]
        [HarmonyPrefix]
        private static void PushTruckClientRpcPatch(VehicleController __instance)
        {
            __instance.pushForceMultiplier = CompanyCruiserConfig.pushForceMultiplier;
        }
    }
}
