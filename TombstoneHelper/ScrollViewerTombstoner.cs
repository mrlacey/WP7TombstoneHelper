using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class ScrollViewerTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<ScrollViewer>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)
                                                 && (o.VerticalOffset > 0)))
            {
                toSaveFrom.State.Add(string.Format("ScrollViewer^{0}", o.Name), o.VerticalOffset);
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var sv = toRestoreTo.ChildrenOfType<ScrollViewer>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            ScheduleOnNextRender(() => sv.ScrollToVerticalOffset(double.Parse(toRestoreTo.State[stateKey].ToString())));
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