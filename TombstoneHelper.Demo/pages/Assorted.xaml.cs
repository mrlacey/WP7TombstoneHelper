using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class Assorted : PhoneApplicationPage
    {
        public Assorted()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.SaveState(); // By not specifying any types all known ones will be supported
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.RestoreState();
        }
    }
}