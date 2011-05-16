using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
            base.OnNavigatingFrom(e);

            TombstoneHelperExtensibility.RegisterCustomTombstoner(typeof(Panorama), new PanoramaTombstoner());
            this.SaveState(e, 1, typeof(Panorama));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            TombstoneHelperExtensibility.RegisterCustomTombstoner(typeof(Panorama), new PanoramaTombstoner());
            this.RestoreState();
        }
    }
}