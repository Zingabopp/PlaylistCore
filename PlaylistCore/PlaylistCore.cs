using System.Linq;
using BS_Utils.Utilities;
using BeatSaberPlaylistsLib;
using PlaylistCore.Overrides;
using System.Collections.Generic;

namespace PlaylistCore
{
    public static class PlaylistCore
    {
        internal static IAnnotatedBeatmapLevelCollection[] playlistTabData;

        public static void SetupCustomPlaylists(object tabBar)
        {
            var manager = BeatSaberPlaylistsLib.PlaylistManager.DefaultManager;
            var playlists = manager.GetAllPlaylists();

            List<IAnnotatedBeatmapLevelCollection> levelCollections = new List<IAnnotatedBeatmapLevelCollection>();
            levelCollections.AddRange(playlistTabData);

            var groups = manager.GetChildManagers();
            for (int i = 0; i < groups.Count(); i++)
            {
                var customGroup = new CustomPlaylistGroup(groups.ElementAt(i));
                levelCollections.Add(customGroup);
            }
            levelCollections.AddRange(playlists);
            tabBar.SetField("annotatedBeatmapLevelCollections", levelCollections.ToArray());
        }

        internal static IAnnotatedBeatmapLevelCollection[] SetupGroup(PlaylistManager manager, bool withBackButton)
        {
            List<IAnnotatedBeatmapLevelCollection> levelCollections = new List<IAnnotatedBeatmapLevelCollection>();
            if (withBackButton)
            {
                var backButton = new CustomPlaylistBackButton(manager.Parent);
                levelCollections.Add(backButton);
            }
            var groups = manager.GetChildManagers();
            for (int i = 0; i < groups.Count(); i++)
            {
                var customGroup = new CustomPlaylistGroup(groups.ElementAt(i));
                levelCollections.Add(customGroup);
            }
            levelCollections.AddRange(manager.GetAllPlaylists());
            return levelCollections.ToArray();
        }
    }
}