using System.Windows;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public class PivotTombstoner : ICanTombstone
    {
        // A pivot should never be inside another pivot so we can ignore the pivotItemIndex
        // It should always be -1
        public void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom)
        {
            if (element is Pivot)
            {
                var pvt = element as Pivot;

                if (!string.IsNullOrEmpty(pvt.Name))
                {
                    if (pvt.SelectedIndex > 0)
                    {
                        toSaveFrom.State.Add(string.Format("Pivot^{0}^-1", pvt.Name), pvt.SelectedIndex);
                    }
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is Pivot)
            {
                (toRestoreTo as Pivot).SelectedIndex = int.Parse(details.ToString());
            }
        }
    }
}
