using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Interfaces
{
    public interface IDeviceSpecific
    {
        Task<bool> CompartilharDados(string titulo, string dados);
        void CloseApplication();
    }
}
