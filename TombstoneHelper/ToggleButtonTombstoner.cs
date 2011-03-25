using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public class ToggleButtonTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is ToggleButton)
            {
                var tb = element as ToggleButton;

                if (!string.IsNullOrEmpty(tb.Name)
                 && (tb.IsChecked ?? false))
                {
                    toSaveFrom.State.Add(string.Format("ToggleButton^{0}", tb.Name), true);
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
