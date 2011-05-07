using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class PasswordBoxTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom)
        {
            if (element is PasswordBox)
            {
                var pb = element as PasswordBox;

                if (!string.IsNullOrEmpty(pb.Name)
                 && !pb.Name.Equals("DisabledContent")
                 && !string.IsNullOrEmpty(pb.Password))
                {
                    toSaveFrom.State.Add(string.Format("PasswordBox^{0}^{1}", pb.Name, pivotItemIndex), pb.Password);
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is PasswordBox)
            {
                (toRestoreTo as PasswordBox).Password = details.ToString();
            }
        }
    }
}