using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class PasswordBoxTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<PasswordBox>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)
                                                 && !o.Name.Equals("DisabledContent")
                                                 && !string.IsNullOrEmpty(o.Password)))
            {
                toSaveFrom.State.Add(string.Format("PasswordBox^{0}", o.Name), o.Password);
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var pb = toRestoreTo.ChildrenOfType<PasswordBox>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            pb.Password = toRestoreTo.State[stateKey].ToString();
        }
    }
}