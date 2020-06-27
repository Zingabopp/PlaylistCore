using System.IO;
using UnityEngine;
using System.Threading;
using BeatSaberPlaylistsLib;
using System.Threading.Tasks;

namespace PlaylistCore.Overrides
{
    public class CustomPlaylistGroup : IPlaylist
    {
        public CustomPlaylistGroup(PlaylistManager manager)
        {
            PlaylistManager = manager;
        }

        public string collectionName => new DirectoryInfo(PlaylistManager.PlaylistPath).Name;

        public Sprite coverImage => Utilities.groupIcon;

        public IBeatmapLevelCollection beatmapLevelCollection => new BeatmapLevelCollection(new DummyPreviewBeatmapLevel[0]);

        public PlaylistManager PlaylistManager { get; }

        internal class DummyPreviewBeatmapLevel : IPreviewBeatmapLevel
        {
            public string levelID => "";

            public string songName => "";

            public string songSubName => "";

            public string songAuthorName => "";

            public string levelAuthorName => "";

            public float beatsPerMinute => 0f;

            public float songTimeOffset => 0f;

            public float shuffle => 0f;

            public float shufflePeriod => 0f;

            public float previewStartTime => 0f;

            public float previewDuration => 0f;

            public float songDuration => 0f;

            public EnvironmentInfoSO environmentInfo => null;

            public EnvironmentInfoSO allDirectionsEnvironmentInfo => null;

            public PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets => null;

            public Task<Texture2D> GetCoverImageTexture2DAsync(CancellationToken cancellationToken)
            {
                return null;
            }

            public Task<AudioClip> GetPreviewAudioClipAsync(CancellationToken cancellationToken)
            {
                return null;
            }
        }
    }
}