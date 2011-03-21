﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class TextBoxTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is TextBox)
            {
                var tb = element as TextBox;

                if (!string.IsNullOrEmpty(tb.Name)
                 && !tb.Name.Equals("DisabledOrReadonlyContent")
                 && !string.IsNullOrEmpty(tb.Text))
                {
                    toSaveFrom.State.Add(string.Format("TextBox^{0}", tb.Name), tb.Text);
                }
            }
        }

        public void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var cb = toRestoreTo.ChildrenOfType<TextBox>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            cb.Text = toRestoreTo.State[stateKey].ToString();
        }
    }
}