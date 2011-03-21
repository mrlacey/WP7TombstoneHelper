using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class RadioButtonTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is RadioButton)
            {
                var rb = element as RadioButton;

                if (!string.IsNullOrEmpty(rb.Name)
                    && (rb.IsChecked ?? false))
                {
                    toSaveFrom.State.Add(string.Format("RadioButton^{0}", rb.Name), true);
                }
            }
        }

        public void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var rb = toRestoreTo.ChildrenOfType<RadioButton>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            rb.IsChecked = true;
        }
    }
}