using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper.Demo.pages
{
    public partial class ScrollViewerExample : PhoneApplicationPage
    {
        public ScrollViewerExample()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            // We know there's only one so we can improve performance by stopping after we find it
            // These 2 methods are identical
            this.SaveState<ScrollViewer>(e, 1);
            //this.SaveState(e, 1, typeof(ScrollViewer));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}