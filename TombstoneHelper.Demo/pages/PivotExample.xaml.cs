using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class PivotExample : PhoneApplicationPage
    {
        public PivotExample()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            this.SaveState(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();
        }
    }
}