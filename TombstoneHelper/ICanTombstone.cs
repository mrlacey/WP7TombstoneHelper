﻿using System.Windows;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal interface ICanTombstone
    {
        void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom);

        void Restore(FrameworkElement toRestoreTo, object details);
    }
}