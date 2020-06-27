using HMUI;
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Notify;
using System.Runtime.CompilerServices;
using BeatSaberMarkupLanguage.Attributes;

namespace PlaylistCore.UI
{
    public class PlaylistSegmentHost : INotifiableHost
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LevelFilteringNavigationController _levelFilterController;

        [UIComponent("segmenter")]
        public TextSegmentedControl segmenter;

        private bool _showGroups = false;
        [UIValue("show-groups")]
        public bool ShowGroups
        {
            get => _showGroups;
            set
            {
                _showGroups = value;
                NotifyPropertyChanged();
            }
        }

        [UIAction("#post-parse")]
        private void Parsed()
        {
            segmenter.SetTexts(new string[] { "BASE GAME", "UNSORTED", "BEATSYNC", "RANKED" });
            /*
            var tabbar = Utilities.TabBar(ref _levelFilterController);
            var cellnew = tabbar.GetComponentInChildren<TextSegmentedControlCellNew>();
            Color color = Utilities.NormalCellBGColor(ref cellnew);
            Plugin.Log.Info(color.ToString());

            var cellnew2 = _segmenter.GetComponentInChildren<TextSegmentedControlCellNew>();
            Color color2 = Utilities.NormalCellBGColor(ref cellnew2);
            Plugin.Log.Info(color2.ToString());*/
        }

        [UIAction("selected-group")]

        public void Setup()
        {
            // Find the navigation controller
            _levelFilterController = Resources.FindObjectsOfTypeAll<LevelFilteringNavigationController>().FirstOrDefault();
            
            BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(
                Assembly.GetExecutingAssembly(),
                "PlaylistCore.UI.playlist-segment.bsml"),
                _levelFilterController.gameObject,
                this);

            
        }

        public void TabSwitched(bool selectedToPlaylist)
        {
            ShowGroups = selectedToPlaylist;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"Error Invoking PropertyChanged: {ex.Message}");
                Plugin.Log.Error(ex);
            }
        }
    }
}