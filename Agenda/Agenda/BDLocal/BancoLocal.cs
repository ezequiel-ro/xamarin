using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Agenda.Interfaces;
using Xamarin.Forms;

namespace Agenda.BDLocal
{
    public class BancoLocal
    {
        private SQLiteConnection dbConnection;
        public SQLiteConnection DBConnection
        {
            get { return dbConnection; }
        }

        public BancoLocal()
        {
            // Conecta ao banco de dados local(ou cria se ele não existir)
            dbConnection = DependencyService.Get<ISQLiteConnection>().DBConnection();
            
            //Cria ou altera as tabelas, se for necessário
            dbConnection.CreateTable<AgendaTable>();
        }
    }
}
