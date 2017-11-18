using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Agenda.Interfaces
{
    public interface ISQLiteConnection
    {
        SQLiteConnection DBConnection();
     }
}
