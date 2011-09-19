using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class PanoramaExample : PhoneApplicationPage
    {
        public PanoramaExample()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            TombstoneHelperExtensibility.RegisterCustomTombstoner(typeof(Panorama), new PanoramaTombstoner());
            this.SaveState(e, 2, typeof(Panorama), typeof(ListBox));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            TombstoneHelperExtensibility.RegisterCustomTombstoner(typeof(Panorama), new PanoramaTombstoner());
            this.RestoreState();
        }
    }
}