using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class Assorted : PhoneApplicationPage
    {
        public Assorted()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState(e); // By not specifying any types all known ones will be supported
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}