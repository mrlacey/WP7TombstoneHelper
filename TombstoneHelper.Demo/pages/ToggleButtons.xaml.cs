using System.Windows.Controls.Primitives;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class ToggleButtons : PhoneApplicationPage
    {
        public ToggleButtons()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState(e, typeof(ToggleButton));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}