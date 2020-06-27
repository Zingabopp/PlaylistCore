using HarmonyLib;
using System.Linq;
using PlaylistCore.Overrides;
using System.Collections.Generic;
using BeatSaberPlaylistsLib;
using HMUI;

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

                    var groups = manager.GetChildManagers;
                    for (int i = 0; i < groups.Count(); i++)
                    {
                        var customGroup = new CustomPlaylistGroup(groups.ElementAt(i));
                        levelCollections.Add(customGroup);
                    }
                    levelCollections.AddRange(playlists);
                    ____annotatedBeatmapLevelCollections = levelCollections.ToArray();
                }
                else
                {
                    var group = new CustomPlaylistGroup(backButton.parent);
                    ____annotatedBeatmapLevelCollections = SetupNonRootGroup(group);
                }
                __instance.HandleAdditionalContentModelDidInvalidateData();
                ____tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
                ____tableView.ClearSelection();
                return false;
            }
            if (playlist is CustomPlaylistGroup)
            {
                var group = playlist as CustomPlaylistGroup;
                ____annotatedBeatmapLevelCollections =  SetupNonRootGroup(group);
                __instance.HandleAdditionalContentModelDidInvalidateData();
                ____tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
                ____tableView.ClearSelection();
                return false;
            }

            return true;
        }

        internal static IAnnotatedBeatmapLevelCollection[] SetupNonRootGroup(CustomPlaylistGroup group)
        {
            List<IAnnotatedBeatmapLevelCollection> levelCollections = new List<IAnnotatedBeatmapLevelCollection>();
            var backButton = new CustomPlaylistBackButton(group.PlaylistManager.Parent);
            levelCollections.Add(backButton);
            var groups = group.PlaylistManager.GetChildManagers;
            for (int i = 0; i < groups.Count(); i++)
            {
                var customGroup = new CustomPlaylistGroup(groups.ElementAt(i));
                levelCollections.Add(customGroup);
            }
            levelCollections.AddRange(group.PlaylistManager.GetAllPlaylists());
            return levelCollections.ToArray();
        }
    }
}