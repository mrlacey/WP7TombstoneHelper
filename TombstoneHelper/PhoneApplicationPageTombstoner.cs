using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    /// <summary>
    /// This Tombstoner behaves and is treated slightly differently to allow restoring the focused control
    /// </summary>
    internal class PhoneApplicationPageTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            Control focusedControl = FocusManager.GetFocusedElement() as Control;

            if (focusedControl != null)
            {
                toSaveFrom.State.Add(string.Format("PhoneApplicationPage^{0}", focusedControl.Name), true);
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            Control focusedControl = toRestoreTo.FindName(details.ToString()) as Control;

            if (focusedControl != null)
            {
                RenderScheduler.ScheduleOnNextRender(() => focusedControl.Focus());
            }
        }
    }
}