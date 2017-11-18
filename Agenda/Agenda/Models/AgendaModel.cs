using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class AgendaModel : IComparable<AgendaModel>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public AgendaModel(int id, string nome, string fone)
        {
            Id = id;
            Nome = nome;
            Telefone = fone;
        }

        public int CompareTo(AgendaModel other)
        {
            return this.Nome.CompareTo(other.Nome);
        }
    }
}
