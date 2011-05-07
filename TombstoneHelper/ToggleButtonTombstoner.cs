using System.Windows;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public class ToggleButtonTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom)
        {
            if (element is ToggleButton)
            {
                var tb = element as ToggleButton;

                if (!string.IsNullOrEmpty(tb.Name)
                 && (tb.IsChecked ?? false))
                {
                    toSaveFrom.State.Add(string.Format("ToggleButton^{0}^{1}", tb.Name, pivotItemIndex), true);
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is ToggleButton)
            {
                (toRestoreTo as ToggleButton).IsChecked = true;
            }
        }
    }
}
