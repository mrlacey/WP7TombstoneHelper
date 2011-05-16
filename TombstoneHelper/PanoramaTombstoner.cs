using System.Windows;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public class PanoramaTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom)
        {
            if (element is Panorama)
            {
                var p = element as Panorama;

                if (!string.IsNullOrEmpty(p.Name) && (p.SelectedIndex > 0))
                {
                    toSaveFrom.SaveStateWithTombstoneHelper(p, pivotItemIndex, p.SelectedIndex, this);
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is Panorama)
            {
               // (toRestoreTo as Panorama).Loaded += (o, args) => { (toRestoreTo as Panorama).DefaultItem = int.Parse(details.ToString()); };
                (toRestoreTo as Panorama).DefaultItem = (toRestoreTo as Panorama).Items[int.Parse(details.ToString())]; 
            }
        }
    }
}
