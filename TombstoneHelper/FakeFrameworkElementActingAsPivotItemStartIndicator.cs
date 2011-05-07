using System.Windows;

namespace TombstoneHelper
{
    internal class FakeFrameworkElementActingAsPivotItemStartIndicator : FrameworkElement
    {
        public int PivotItemIndex { get; set; }

        public FakeFrameworkElementActingAsPivotItemStartIndicator(int pivotItemIndex)
        {
            this.PivotItemIndex = pivotItemIndex;
        }
    }
}