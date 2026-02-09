using BepInEx;
using BepInEx.Logging;
using CompanyCruiserConfig.Patches;
using HarmonyLib;

namespace CompanyCruiserConfig
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class CompanyCruiserConfig : BaseUnityPlugin
    {
        public static CompanyCruiserConfig Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        // general
        public static bool editRigidbody;
        public static bool despawnInVoid;
        public static float despawnHeightThreshold;
        public static bool editEnemyCollisionDamage;

        // vehiclecontroller
        public static int baseCarHP; // 30
        public static float brakeSpeed; // 500
        public static float carAcceleration; // 250
        public static float carFragility; // 1
        public static float carHitPlayerForceFraction; // 30
        public static float carMaxSpeed; // 600
        public static float carReactToPlayerHitMultiplier; // 2850
        public static float engineIntensityPercentage; // 180
        public static float engineTorque; // 1100
        public static float idleSpeed; // 15
        public static float jumpForce; // 600
        public static float maxEngineRPM; // 3000
        public static float maximumBumpForce; // 75000
        public static float mediumBumpForce; // 30000
        public static float minEngineRPM; // 1000
        public static float minimalBumpForce; // 9000
        public static int movingAverageLength; // 20
        public static float pushForceMultiplier; // 27
        public static float pushVerticalOffsetAmount; // 1
        public static float radioSignalDecreaseThreshold; // 50
        public static float radioSignalQuality; // 3
        public static float radioSignalTurbulence; // 4
        public static float speed; // 50
        public static float springForce; // 130
        public static float stability; // 0.4
        public static float steeringWheelTurnSpeed; // 4
        public static float syncRotationSpeed; // 0.2
        public static float syncSpeedMultiplier; // 10
        public static float torqueForce; // 2.5
        public static float turboBoostForce; // 3000
        public static float turboBoostUpwardForce; // 7200

        // rigidbody
        public static float angularDrag;
        public static float drag;
        public static float mass;
        public static float maxAngularVelocity;
        public static float maxDepenetrationVelocity;
        public static float maxLinearVelocity;
        public static float sleepThreshold;
        public static int solverIterations;
        public static int solverVelocityIterations;

        // enemy collision damage
        public static int smallHit;
        public static int largeHit;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            InitializeConfigs();
            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal void InitializeConfigs()
        {
            // general
            editRigidbody = Config.Bind("General", "Edit Rigidbody", false, "Should configuration for the main rigidbody in VehicleController be enabled?").Value;
            despawnInVoid = Config.Bind("General", "Despawn In Void", true, "Should any VehicleController object that goes below Despawn Height Threshold be despawned? This check runs in FixedUpdate()").Value;
            despawnHeightThreshold = Config.Bind("General", "Despawn Height Threshold", -500f, "Threshold where any VehicleController object below this height will be despawned.").Value;
            editEnemyCollisionDamage = Config.Bind("General", "Edit Enemy Collision Damage", false, "Should configuration of enemy collision damage values be enabled?").Value;

            // vehiclecontroller
            baseCarHP = Config.Bind("VehicleController", "Base Car HP", 30, "Initial car hp.").Value;
            brakeSpeed = Config.Bind("VehicleController", "Brake Speed", 500f, "Vehicle braking speed.").Value;
            carAcceleration = Config.Bind("VehicleController", "Car Acceleration", 250f,"").Value;
            carFragility = Config.Bind("VehicleController", "Car Fragility", 1f, "").Value;
            carHitPlayerForceFraction = Config.Bind("VehicleController", "Car Hit Player Force Fraction", 30f, "").Value;
            carMaxSpeed = Config.Bind("VehicleController", "Car Max Speed", 600f, "").Value;
            carReactToPlayerHitMultiplier = Config.Bind("VehicleController", "Car React To Player Hit Multiplier", 2850f, "").Value;
            engineIntensityPercentage = Config.Bind("VehicleController", "Engine Intensity Percentage", 180f, "").Value;
            engineTorque = Config.Bind("VehicleController", "Engine Torque", 1100f, "").Value;
            idleSpeed = Config.Bind("VehicleController", "Idle Speed", 15f, "").Value;
            jumpForce = Config.Bind("VehicleController", "Jump Force", 600f, "").Value;
            maxEngineRPM = Config.Bind("VehicleController", "Max Engine RPM", 3000f, "").Value;
            maximumBumpForce = Config.Bind("VehicleController", "Maximum Bump Force", 75000f, "").Value;
            mediumBumpForce = Config.Bind("VehicleController", "Medium Bump Force", 30000f, "").Value;
            minEngineRPM = Config.Bind("VehicleController", "Min Engine RPM", 1000f, "").Value;
            minimalBumpForce = Config.Bind("VehicleController", "Minimal Bump Force", 9000f, "").Value;
            movingAverageLength = Config.Bind("VehicleController", "Moving Average Length", 20, "").Value;
            pushForceMultiplier = Config.Bind("VehicleController", "Push Force Multiplier", 27.0f, "Multiplier of the force to push the cruiser.").Value;
            pushVerticalOffsetAmount = Config.Bind("VehicleController", "Push Vertical Offset Amount", 1f, "").Value;
            radioSignalDecreaseThreshold = Config.Bind("VehicleController", "Radio Signal Decrease Threshold", 50f, "").Value;
            radioSignalQuality = Config.Bind("VehicleController", "Radio Signal Quality", 3f, "").Value;
            radioSignalTurbulence = Config.Bind("VehicleController", "Radio Signal Turbulence", 4f, "").Value;
            speed = Config.Bind("VehicleController", "Speed", 50f, "").Value;
            springForce = Config.Bind("VehicleController", "Spring Force", 130f, "").Value;
            stability = Config.Bind("VehicleController", "Stability", 0.4f, "").Value;
            steeringWheelTurnSpeed = Config.Bind("VehicleController", "Steering Wheel Turn Speed", 4f, "").Value;
            syncRotationSpeed = Config.Bind("VehicleController", "Sync Rotation Speed", 0.2f, "").Value;
            syncSpeedMultiplier = Config.Bind("VehicleController", "Sync Speed Multiplier", 10f, "").Value;
            torqueForce = Config.Bind("VehicleController", "Torque Force", 2.5f, "").Value;
            turboBoostForce = Config.Bind("VehicleController", "Turbo Boost Force", 3000f, "").Value;
            turboBoostUpwardForce = Config.Bind("VehicleController", "Turbo Boost Upward Force", 7200f, "").Value;

            // rigidbody
            angularDrag = Config.Bind("Rigidbody", "Angular Drag", 0.5f, "").Value;
            drag = Config.Bind("Rigidbody", "Drag", 0.01f, "").Value;
            mass = Config.Bind("Rigidbody", "Mass", 200f, "").Value;
            maxAngularVelocity = Config.Bind("Rigidbody", "Max Angular Velocity", 4f, "").Value;
            maxDepenetrationVelocity = Config.Bind("Rigidbody", "Max Depenetration Velocity", 1f, "").Value;
            maxLinearVelocity = Config.Bind("Rigidbody", "Max Linear Velocity", 50f, "").Value;
            sleepThreshold = Config.Bind("Rigidbody", "Sleep Threshold", 0.005f, "").Value;
            solverIterations = Config.Bind("Rigidbody", "Solver Iterations", 16, "").Value;
            solverVelocityIterations = Config.Bind("Rigidbody", "Solver Velocity Iterations", 2, "").Value;

            // enemy collision damage
            smallHit = Config.Bind("Collision", "Small Hit", 2, "").Value;
            largeHit = Config.Bind("Collision", "Large Hit", 12, "").Value;
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll(typeof(VehicleControllerPatch));

            if (despawnInVoid)
            {
                Harmony.PatchAll(typeof(VehicleControllerDestroyCruiserPatch));
            }

            if (editEnemyCollisionDamage)
            {
                Harmony.PatchAll(typeof(CarReactToObstaclePatch));
            }

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
