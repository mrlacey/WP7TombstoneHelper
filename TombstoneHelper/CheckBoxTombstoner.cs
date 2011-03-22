using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class CheckBoxTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is CheckBox)
            {
                var cb = element as CheckBox;

                if (!string.IsNullOrEmpty(cb.Name)
                 && (cb.IsChecked ?? false))
                {
                    toSaveFrom.State.Add(string.Format("CheckBox^{0}", cb.Name), true);
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is CheckBox)
            {
                (toRestoreTo as CheckBox).IsChecked = true;
            }
        }
    }
}