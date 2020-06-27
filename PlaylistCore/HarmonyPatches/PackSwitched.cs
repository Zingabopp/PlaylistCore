using HarmonyLib;

namespace PlaylistCore.HarmonyPatches
{
    [HarmonyPatch(typeof(LevelFilteringNavigationController), "TabBarDidSwitch")]
    internal class PackSwitched
    {
        internal static void Postfix(ref TabBarViewController ____tabBarViewController)
        {
            //Utilities.tabSwitch?.Invoke(____tabBarViewController.selectedCellNumber);
        }
    }
}