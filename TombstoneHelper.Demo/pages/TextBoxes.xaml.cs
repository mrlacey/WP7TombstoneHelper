﻿using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class TextBoxes : PhoneApplicationPage
    {
        public TextBoxes()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState(e, typeof(TextBox));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}