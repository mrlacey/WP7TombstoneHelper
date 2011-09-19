using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class HorizontalScrollViewer : PhoneApplicationPage
    {
        public HorizontalScrollViewer()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState(e, 1, typeof(ScrollViewer));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}