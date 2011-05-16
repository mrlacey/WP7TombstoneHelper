using System.Windows;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public interface ICanTombstone
    {
        void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom);

        void Restore(FrameworkElement toRestoreTo, object details);
    }
}