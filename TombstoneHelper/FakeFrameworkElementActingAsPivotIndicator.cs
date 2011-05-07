using System.Windows;

namespace TombstoneHelper
{
    // This exists as we need to know if the Page contains a Pivot
    // even if we're not looking to save the state of a pivot
    // and without walking the visual tree more than once (as it can impact performance)
    // If you have a better solution please let me know :)
    internal class FakeFrameworkElementActingAsPivotIndicator : FrameworkElement
    {
    }
}