using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public static class TombstoneHelperExtensibility
    {
        private static Dictionary<Type, ICanTombstone> customTombstoners = new Dictionary<Type, ICanTombstone>();

        internal static Dictionary<Type, ICanTombstone> CustomTombstoners
        {
            get { return customTombstoners; }
            set { customTombstoners = value; }
        }

        public static void RegisterCustomTombstoner(Type type, ICanTombstone customTombstoner)
        {
            if (!customTombstoners.ContainsKey(type))
            {
                customTombstoners.Add(type, customTombstoner);
            }
        }

        public static void SaveStateWithTombstoneHelper(this PhoneApplicationPage page, FrameworkElement element, int pivotItemIndex, object stateValue, ICanTombstone restorer)
        {
            page.State.Add(string.Format("{0}^{1}^{2}", element.GetType().Name, element.Name, pivotItemIndex), stateValue);
        }
    }
}