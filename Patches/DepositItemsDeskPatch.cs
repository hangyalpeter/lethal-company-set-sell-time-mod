using HarmonyLib;

namespace SetCompanySellTime.Patches
{
    [HarmonyPatch(typeof(DepositItemsDesk))]
    internal class DepositItemsDeskPatch
    {
        [HarmonyPatch(nameof(DepositItemsDesk.AddObjectToDeskServerRpc))]
        [HarmonyPostfix]
        static void patchAddObjectToDeskServerRpc(ref float ___grabObjectsTimer)
        {
            ___grabObjectsTimer = DepositItemsDeskModBase.configSellTime.Value;
        }

        [HarmonyPatch(nameof(DepositItemsDesk.SetPatienceServerRpc))]
        [HarmonyPostfix]
        static void patchSetPatienceServerRpc(ref CompanyMood ___currentMood)
        {
            ___currentMood.judgementSpeed = 0f;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void patchDoorShutAfterSell(ref float ___waitingWithDoorOpenTimer)
        {
            ___waitingWithDoorOpenTimer = 20f;
        }
    }
}
