using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class ListBoxTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<ListBox>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)))
            {
                var sv = o.ChildrenOfType<ScrollViewer>().First();

                if (sv.VerticalOffset > 0)
                {
                    toSaveFrom.State.Add(string.Format("ListBox^{0}", o.Name), sv.VerticalOffset);
                }
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var lb = toRestoreTo.ChildrenOfType<ListBox>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            lb.ApplyTemplate();

            var sv = lb.ChildrenOfType<ScrollViewer>()
                       .FirstOrDefault();

            ScrollViewerTombstoner.ScheduleOnNextRender(() => sv.ScrollToVerticalOffset(double.Parse(toRestoreTo.State[stateKey].ToString())));
        }
    }
}
