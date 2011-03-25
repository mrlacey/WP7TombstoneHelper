﻿using System.Windows.Controls.Primitives;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class ToggleButtons : PhoneApplicationPage
    {
        public ToggleButtons()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.SaveState(typeof(ToggleButton));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();
        }
    }
}