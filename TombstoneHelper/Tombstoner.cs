using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    internal abstract class Tombstoner
    {
        internal abstract void Save(PhoneApplicationPage toSaveFrom);

        internal abstract void Restore(PhoneApplicationPage toRestoreTo, string stateKey);
    }
}