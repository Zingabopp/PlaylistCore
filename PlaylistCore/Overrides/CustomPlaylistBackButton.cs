using UnityEngine;
using BeatSaberPlaylistsLib;
using static PlaylistCore.Overrides.CustomPlaylistGroup;

namespace PlaylistCore.Overrides
{
    public class CustomPlaylistBackButton : IPlaylist
    {
        
        public CustomPlaylistBackButton(PlaylistManager parent)
        {
            this.parent = parent;
        }

        public PlaylistManager parent;

        public string collectionName => "Back";

        public Sprite coverImage => Utilities.backButton;

        public IBeatmapLevelCollection beatmapLevelCollection => new BeatmapLevelCollection(new DummyPreviewBeatmapLevel[0]);
    }
}