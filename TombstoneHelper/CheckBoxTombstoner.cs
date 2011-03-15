using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class CheckBoxTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<CheckBox>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)
                                                 && (o.IsChecked ?? false)))
            {
                toSaveFrom.State.Add(string.Format("CheckBox^{0}", o.Name), true);
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var tb = toRestoreTo.ChildrenOfType<CheckBox>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            tb.IsChecked = true;
        }
    }
}