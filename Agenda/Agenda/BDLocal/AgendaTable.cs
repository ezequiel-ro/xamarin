using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Agenda.Models;

namespace Agenda.BDLocal
{
    [Table("agendas")]
    public class AgendaTable
    {
        [PrimaryKey, Column("id"), AutoIncrement]
        public int Id { get; set; }

        [MaxLength(80), Column("nome")]
        public string Nome { get; set; }

        [MaxLength(15), Column("telefone")]
        public string Telefone { get; set; }

        static public List<AgendaModel> GetTelefones()
        {
            try
            {
                string strQuery = "SELECT * FROM [agendas]";
                List<AgendaTable> lst = App.BDLocal.DBConnection.Query<AgendaTable>(strQuery);

                if (lst.Count == 0)
                {
                    return null;
                }

                AgendaModel novo;
                List<AgendaModel> lstModel = new List<AgendaModel>();
                foreach (AgendaTable a in lst)
                {
                    novo = new AgendaModel(a.Id, a.Nome, a.Telefone);
                    lstModel.Add(novo);
                }

                //ordena a lista em ordem alfabetica
                lstModel.Sort();

                return lstModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        static public bool InsertUpdateDados(int id, string nome, string fone)
        {
            try
            {
                AgendaTable ag = new AgendaTable();
                ag.Id = id;
                ag.Nome = nome;
                ag.Telefone = fone;

                if (id == 0)
                    App.BDLocal.DBConnection.Insert(ag);
                else
                    App.BDLocal.DBConnection.Update(ag);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static public bool EliminaRegistro(int id)
        {
            try
            {
                string strQuery = "DELETE FROM [agendas] where id = " + id.ToString();
                App.BDLocal.DBConnection.Execute(strQuery);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
