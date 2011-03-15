using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal class TextBoxTombstoner : Tombstoner
    {
        internal override void Save(PhoneApplicationPage toSaveFrom)
        {
            foreach (var o in toSaveFrom.ChildrenOfType<TextBox>()
                                        .Where(o => !string.IsNullOrEmpty(o.Name)
                                                 && !o.Name.Equals("DisabledOrReadonlyContent")
                                                 && !string.IsNullOrEmpty(o.Text)))
            {
                toSaveFrom.State.Add(string.Format("TextBox^{0}", o.Name), o.Text);
            }
        }

        internal override void Restore(PhoneApplicationPage toRestoreTo, string stateKey)
        {
            var cb = toRestoreTo.ChildrenOfType<TextBox>()
                                .First(o => o.Name.Equals(stateKey.Split('^')[1]));

            cb.Text = toRestoreTo.State[stateKey].ToString();
        }
    }
}