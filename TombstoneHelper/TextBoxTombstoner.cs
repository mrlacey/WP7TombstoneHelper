using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class TextBoxTombstoner : ICanTombstone
    {
        /// Note that this Tombstoner uses the '✆' character as a separator
        /// - It should be very unlikely that this character will ever be in user entered text but even if it is we handle that too. ;)
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
                                         string.Format("{0}✆{1}✆{2}", tb.Text,
                                                                      tb.SelectionStart,
                                                                      tb.SelectionLength));
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is TextBox)
            {
                var detail = details.ToString().Split('✆');

                string textToRestore = string.Empty;

                for (int i = 0; i < detail.Length - 2; i++)
                {
                    textToRestore += detail[i] + "✆";
                }

                (toRestoreTo as TextBox).Text = textToRestore.TrimEnd('✆');

                (toRestoreTo as TextBox).SelectionStart = int.Parse(detail[detail.Length - 2]);
                (toRestoreTo as TextBox).SelectionLength = int.Parse(detail[detail.Length - 1]);
            }
        }
    }
}