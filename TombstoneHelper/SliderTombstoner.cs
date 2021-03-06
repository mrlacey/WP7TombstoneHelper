﻿using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class SliderTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, int pivotItemIndex, PhoneApplicationPage toSaveFrom)
        {
            if (element is Slider)
            {
                var s = element as Slider;

                if (!string.IsNullOrEmpty(s.Name)
                    && (s.Value > s.Minimum))
                {
                    toSaveFrom.State.Add(string.Format("Slider^{0}^{1}", s.Name, pivotItemIndex), s.Value);
                }
            }
        }

        public void Restore(FrameworkElement toRestoreTo, object details)
        {
            if (toRestoreTo is Slider)
            {
                (toRestoreTo as Slider).Value = double.Parse(details.ToString());
            }
        }
    }
}