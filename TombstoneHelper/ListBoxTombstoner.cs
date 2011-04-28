using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class ListBoxTombstoner : ICanTombstone
    {
        // TODO: also add storing of selected item
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is ListBox)
            {
                var lb = element as ListBox;

                if (!string.IsNullOrEmpty(lb.Name))
                {
                    var sv = lb.ChildrenOfType<ScrollViewer>().First();

                    if (sv.VerticalOffset > 0)
                    {
                        toSaveFrom.State.Add(string.Format("ListBox^{0}", lb.Name), sv.VerticalOffset);
                    }
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is ListBox)
            {
                (toRestoreTo as ListBox).ApplyTemplate();

                var sv = (toRestoreTo as ListBox).ChildrenOfType<ScrollViewer>()
                                                 .FirstOrDefault();

                RenderScheduler.ScheduleOnNextRender(() => sv.ScrollToVerticalOffset(double.Parse(details.ToString())));
            }
        }
    }
}
