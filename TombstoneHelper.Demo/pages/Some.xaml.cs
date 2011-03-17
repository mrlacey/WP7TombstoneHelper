﻿using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class Some : PhoneApplicationPage
    {
        public Some()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Just the types specified will be checked and saved
            // this list is just the ones used on the page and so is faster than checking all types
            this.SaveState(typeof(TextBox), typeof(PasswordBox), typeof(CheckBox));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is just a demo of tombstoning functionality", "just a demo", MessageBoxButton.OK);
        }
    }
}