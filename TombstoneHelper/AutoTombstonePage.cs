using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    /// <summary>
    /// AutoTombstonePage is an example of how to encapsulate the TombstoneHelper functionality
    /// into a common class which can be used as an alternative to PhoneApplicationPage
    /// for your pages. This will automatically store all state when tombstoned.
    /// The downside is that you can't use the OnNavigated[To|From] events yourself. :(
    /// </summary>
    public class AutoTombstonePage : PhoneApplicationPage
    {
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SaveState(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.RestoreState();
        }
    }
}