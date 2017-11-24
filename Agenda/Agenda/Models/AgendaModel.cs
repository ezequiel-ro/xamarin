using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agenda.Models
{
    public class AgendaModel : IComparable<AgendaModel>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Imagem { get; set; }

        public AgendaModel(int id, string nome, string fone, string imagem)
        {
            Id = id;
            Nome = nome;
            Telefone = fone;
            Imagem = imagem;
        }

        public int CompareTo(AgendaModel other)
        {
            return this.Nome.CompareTo(other.Nome);
        }
    }
}
