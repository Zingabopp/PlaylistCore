using UnityEngine;

namespace PlaylistCore.Overrides
{
    public class CustomPlaylist : IPlaylist
    {
        private BeatSaberPlaylistsLib.Types.IPlaylist _playlist;

        public CustomPlaylist(BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {
            _playlist = playlist;
        }

        public string collectionName => _playlist.collectionName;

        public Sprite coverImage => _playlist.coverImage;

        public IBeatmapLevelCollection beatmapLevelCollection => _playlist.beatmapLevelCollection;
    }
}
