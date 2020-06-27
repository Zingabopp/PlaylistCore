using HMUI;
using UnityEngine;
using IPA.Utilities;
using System.Reflection;

namespace PlaylistCore
{
    internal static class Utilities
    {
        internal static FieldAccessor<TextSegmentedControlCellNew, Color>.Accessor NormalCellBGColor = FieldAccessor<TextSegmentedControlCellNew, Color>.GetAccessor("_normalBGColor");
        internal static FieldAccessor<LevelFilteringNavigationController, TabBarViewController>.Accessor TabBar = FieldAccessor<LevelFilteringNavigationController, TabBarViewController>.GetAccessor("_tabBarViewController");

        internal static Sprite groupIcon = BeatSaberMarkupLanguage.Utilities.LoadSpriteRaw(BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "PlaylistCore.Resources.GroupIcon.png"));
        internal static Sprite backButton = BeatSaberMarkupLanguage.Utilities.LoadSpriteRaw(BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "PlaylistCore.Resources.BackArrow.png"));
    }
}