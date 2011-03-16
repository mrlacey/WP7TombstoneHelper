using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class SliderTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<Slider>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)
                                                 && (o.Value > o.Minimum)))
            {
                toSaveFrom.State.Add(string.Format("Slider^{0}", o.Name), o.Value);
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var s = toRestoreTo.ChildrenOfType<Slider>()
                               .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            s.Value = double.Parse(toRestoreTo.State[stateKey].ToString());
        }
    }
}