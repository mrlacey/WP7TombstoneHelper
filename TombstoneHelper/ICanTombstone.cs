using System.Windows;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal interface ICanTombstone
    {
        void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom);

        void Restore(PhoneApplicationPage toRestoreTo, string stateKey);
    }
}