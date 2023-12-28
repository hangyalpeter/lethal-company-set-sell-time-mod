using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SetCompanySellTime.Patches;

namespace SetCompanySellTime
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class DepositItemsDeskModBase : BaseUnityPlugin
    {
        private const string modGUID = "com.peter.lethalcompany.deposititemsdeskmod";
        private const string modName = "Set Company Sell Time";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static DepositItemsDeskModBase instance;

        internal ManualLogSource log;

        public static ConfigEntry<float> configSellTime;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            log = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            configSellTime = Config.Bind("General", "sellTime", 2f, "Time in seconds to sell items (min 1 sec max 10 sec)");

            if (configSellTime.Value < 1f)
            {
                configSellTime.Value = 1f;
            }

            if (configSellTime.Value > 10f)
            {
                configSellTime.Value = 10f;
            }

            log.LogInfo("Set Company Sell Time loaded");

            harmony.PatchAll(typeof(DepositItemsDeskModBase));
            harmony.PatchAll(typeof(DepositItemsDeskPatch));
        }
    }
}
