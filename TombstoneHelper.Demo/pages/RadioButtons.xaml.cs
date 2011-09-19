using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class RadioButtons : PhoneApplicationPage
    {
        public RadioButtons()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState(e, typeof(RadioButton));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}