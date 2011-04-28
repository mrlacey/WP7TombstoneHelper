using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class ScrollViewerTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is ScrollViewer)
            {
                var sv = element as ScrollViewer;

                if (!string.IsNullOrEmpty(sv.Name))
                {
                    if ((sv.HorizontalOffset > 0) || (sv.VerticalOffset > 0))
                    {
                        toSaveFrom.State.Add(string.Format("ScrollViewer^{0}", sv.Name), string.Format("{0}:{1}", sv.HorizontalOffset, sv.VerticalOffset));
                    }
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is ScrollViewer)
            {
                var detail = details.ToString().Split(':');

                if (detail[0] != "0")
                {
                    RenderScheduler.ScheduleOnNextRender(() => (toRestoreTo as ScrollViewer).ScrollToHorizontalOffset(double.Parse(detail[0])));
                }

                if (detail[1] != "0")
                {
                    RenderScheduler.ScheduleOnNextRender(() => (toRestoreTo as ScrollViewer).ScrollToVerticalOffset(double.Parse(detail[1])));
                }
            }
        }
    }
}