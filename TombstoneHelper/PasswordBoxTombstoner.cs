using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class PasswordBoxTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is PasswordBox)
            {
                var pb = element as PasswordBox;

                if (!string.IsNullOrEmpty(pb.Name)
                 && !pb.Name.Equals("DisabledContent")
                 && !string.IsNullOrEmpty(pb.Password))
                {
                    toSaveFrom.State.Add(string.Format("PasswordBox^{0}", pb.Name), pb.Password);
                }
            }
        }

        public void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var pb = toRestoreTo.ChildrenOfType<PasswordBox>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            pb.Password = toRestoreTo.State[stateKey].ToString();
        }
    }
}