using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace TombstoneHelper
{
    internal static class RenderScheduler
    {
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