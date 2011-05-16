using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            this.SaveState(e, typeof(ScrollViewer));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();
        }

        private void TextBoxes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/TextBoxes.xaml", UriKind.Relative));
        }

        private void ListBoxes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/ListBoxes.xaml", UriKind.Relative));
        }

        private void Assorted_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/Assorted.xaml", UriKind.Relative));
        }

        private void Some_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/Some.xaml", UriKind.Relative));
        }

        private void PasswordBoxes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/PasswordBoxes.xaml", UriKind.Relative));
        }

        private void CheckBoxes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/CheckBoxes.xaml", UriKind.Relative));
        }

        private void RadioButtons_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/RadioButtons.xaml", UriKind.Relative));
        }

        private void ScrollViewers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/ScrollViewerExample.xaml", UriKind.Relative));
        }

        private void Sliders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/Sliders.xaml", UriKind.Relative));
        }

        private void AutoTombstonePage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/AutoTombstone.xaml", UriKind.Relative));
        }

        private void ToggleButtons_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/ToggleButtons.xaml", UriKind.Relative));
        }

        private void HorizontalScrollViewers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/HorizontalScrollViewer.xaml", UriKind.Relative));
        }

        private void PivotPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/PivotExample.xaml", UriKind.Relative));
        }

        private void PanoramaPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pages/PanoramaExample.xaml", UriKind.Relative));
        }
    }
}