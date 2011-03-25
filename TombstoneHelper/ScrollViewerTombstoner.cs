using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    // TODO: also add support for horizontal offsets
    internal class ScrollViewerTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is ScrollViewer)
            {
                var sv = element as ScrollViewer;

                if (!string.IsNullOrEmpty(sv.Name)
                    && (sv.VerticalOffset > 0))
                {
                    toSaveFrom.State.Add(string.Format("ScrollViewer^{0}", sv.Name), sv.VerticalOffset);
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is ScrollViewer)
            {
                ScheduleOnNextRender(() => (toRestoreTo as ScrollViewer).ScrollToVerticalOffset(double.Parse(details.ToString())));
            }
        }

        private static List<Action> workItems;

        // See http://msdn.microsoft.com/en-us/library/ff967548(v=vs.92).aspx
        public static void ScheduleOnNextRender(Action action)
        {
            if (workItems == null)
            {
                workItems = new List<Action>();
                CompositionTarget.Rendering += DoWorkOnRender;
            }

            workItems.Add(action);
        }

        internal static void DoWorkOnRender(object sender, EventArgs args)
        {
            CompositionTarget.Rendering -= DoWorkOnRender;
            List<Action> work = workItems;
            workItems = null;

            foreach (Action action in work)
            {
                try
                {
                    action();
                }
                catch { }
            }
        }
    }
}