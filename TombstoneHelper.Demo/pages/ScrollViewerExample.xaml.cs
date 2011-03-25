﻿using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class ScrollViewerExample : PhoneApplicationPage
    {
        public ScrollViewerExample()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.SaveState(1, typeof(ScrollViewer)); // We know there's only one so we can improve performance by stopping after we find it
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();
        }
    }
}