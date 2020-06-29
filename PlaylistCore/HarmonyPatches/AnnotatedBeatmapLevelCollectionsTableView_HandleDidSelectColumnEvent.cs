using HMUI;
using HarmonyLib;
using PlaylistCore.Overrides;
using System.Collections.Generic;
using BeatSaberPlaylistsLib;

namespace PlaylistCore.HarmonyPatches
{
    [HarmonyPatch(typeof(AnnotatedBeatmapLevelCollectionsTableView), "HandleDidSelectColumnEvent")]
    internal class AnnotatedBeatmapLevelCollectionsTableView_HandleDidSelectColumnEvent
    {
        internal static bool Prefix(ref AnnotatedBeatmapLevelCollectionsTableView __instance, ref IAnnotatedBeatmapLevelCollection[] ____annotatedBeatmapLevelCollections, ref int column, ref TableView ____tableView)
        {
            var playlist = ____annotatedBeatmapLevelCollections[column];
            if (playlist is CustomPlaylistBackButton)
            {
                var backButton = playlist as CustomPlaylistBackButton;

                if (backButton.parent == PlaylistManager.DefaultManager)
                {
                    var manager = PlaylistManager.DefaultManager;
                    var playlists = manager.GetAllPlaylists();

                    List<IAnnotatedBeatmapLevelCollection> levelCollections = new List<IAnnotatedBeatmapLevelCollection>();
                    levelCollections.AddRange(PlaylistCore.playlistTabData);
                    levelCollections.AddRange(PlaylistCore.SetupGroup(manager, false));
                    ____annotatedBeatmapLevelCollections = levelCollections.ToArray();
                }
                else
                {
                    var group = new CustomPlaylistGroup(backButton.parent);
                    ____annotatedBeatmapLevelCollections = PlaylistCore.SetupGroup(group.PlaylistManager, true);
                }
                __instance.HandleAdditionalContentModelDidInvalidateData();
                ____tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
                ____tableView.ClearSelection();
                return false;
            }
            if (playlist is CustomPlaylistGroup)
            {
                var group = playlist as CustomPlaylistGroup;
                ____annotatedBeatmapLevelCollections = PlaylistCore.SetupGroup(group.PlaylistManager, true);
                __instance.HandleAdditionalContentModelDidInvalidateData();
                ____tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
                ____tableView.ClearSelection();
                return false;
            }

            return true;
        }
    }
}