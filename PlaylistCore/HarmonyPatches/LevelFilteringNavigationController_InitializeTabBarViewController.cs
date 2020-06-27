using HarmonyLib;
using BS_Utils.Utilities;

namespace PlaylistCore.HarmonyPatches
{
    [HarmonyPatch(typeof(LevelFilteringNavigationController), "InitializeTabBarViewController")]
    internal class LevelFilteringNavigationController_InitializeTabBarViewController
    {
        internal static void Postfix(ref LevelFilteringNavigationController __instance, ref object ____playlistTabBarData)
        {
            PlaylistCore.playlistTabData = (IAnnotatedBeatmapLevelCollection[])____playlistTabBarData.GetField("annotatedBeatmapLevelCollections");

            PlaylistCore.SetupCustomPlaylists(__instance, ____playlistTabBarData);
        }
    }
}