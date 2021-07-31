using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.SimulationObjects.PropertiesControl
{
    public interface IPropControl
    {
        void RefreshValues();
        event Action<object> OnPropertiesChanged;
        event Action<object> OnPropertiesNameChanged;
        void SetObject(object obj);
    }
}
