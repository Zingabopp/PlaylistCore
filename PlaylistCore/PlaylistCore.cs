using System.Linq;
using BS_Utils.Utilities;
using PlaylistCore.Overrides;
using System.Collections.Generic;

namespace PlaylistCore
{
    public static class PlaylistCore
    {
        internal static IAnnotatedBeatmapLevelCollection[] playlistTabData;

        public static void SetupCustomPlaylists(LevelFilteringNavigationController navController, object tabBar)
        {
            var manager = BeatSaberPlaylistsLib.PlaylistManager.DefaultManager;
            var playlists = manager.GetAllPlaylists();

            List<IAnnotatedBeatmapLevelCollection> levelCollections = new List<IAnnotatedBeatmapLevelCollection>();
            levelCollections.AddRange(playlistTabData);

            var groups = manager.GetChildManagers;
            for (int i = 0; i < groups.Count(); i++)
            {
                var customGroup = new CustomPlaylistGroup(groups.ElementAt(i));
                levelCollections.Add(customGroup);
            }
            levelCollections.AddRange(playlists);
            tabBar.SetField("annotatedBeatmapLevelCollections", levelCollections.ToArray());
        }
    }
}