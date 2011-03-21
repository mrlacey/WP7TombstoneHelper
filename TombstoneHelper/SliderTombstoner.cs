using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class SliderTombstoner : ICanTombstone
    {
        public void Save(FrameworkElement element, PhoneApplicationPage toSaveFrom)
        {
            if (element is Slider)
            {
                var s = element as Slider;

                if (!string.IsNullOrEmpty(s.Name)
                    && (s.Value > s.Minimum))
                {
                    toSaveFrom.State.Add(string.Format("Slider^{0}", s.Name), s.Value);
                }
            }
        }

        public void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var s = toRestoreTo.ChildrenOfType<Slider>()
                               .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            s.Value = double.Parse(toRestoreTo.State[stateKey].ToString());
        }
    }
}