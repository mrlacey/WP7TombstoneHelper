using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class RadioButtonTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<RadioButton>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)
                                                 && (o.IsChecked ?? false)))
            {
                toSaveFrom.State.Add(string.Format("RadioButton^{0}", o.Name), true);
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var rb = toRestoreTo.ChildrenOfType<RadioButton>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            rb.IsChecked = true;
        }
    }
}