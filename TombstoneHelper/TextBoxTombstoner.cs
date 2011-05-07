using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class TextBoxTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom)
        {
            if (element is TextBox)
            {
                var tb = element as TextBox;

                if (!string.IsNullOrEmpty(tb.Name)
                    && !tb.Name.Equals("DisabledOrReadonlyContent")
                    && !string.IsNullOrEmpty(tb.Text))
                {
                    toSaveFrom.State.Add(string.Format("TextBox^{0}^{1}", tb.Name, pivotItemIndex),
                                         string.Format("{0}:{1}:{2}", tb.Text,
                                                                      tb.SelectionStart,
                                                                      tb.SelectionLength));
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is TextBox)
            {
                var detail = details.ToString().Split(':');

                (toRestoreTo as TextBox).Text = detail[0];

                (toRestoreTo as TextBox).SelectionStart = int.Parse(detail[1]);
                (toRestoreTo as TextBox).SelectionLength = int.Parse(detail[2]);
            }
        }
    }
}