using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.BDLocal;
using System.ComponentModel;
using Agenda.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaInicial : ContentPage
    {
        private AgendaViewModel viewModel;

        public PaginaInicial()
        {
            InitializeComponent();
            viewModel = new AgendaViewModel();
            this.BindingContext = viewModel;

            //mostrar todas as entradas da agenda
            MostraDados(null);

            //usuario terminou de editar um registro
            MessagingCenter.Subscribe<Application, Models.AgendaModel>(this, "MntDados", (sender, arg) =>
            {
                //atualiza dados no banco de dados local
                AgendaTable.InsertUpdateDados(arg.Id, arg.Nome, arg.Telefone);

                //atualiza lista
                MostraDados(null);
            });

            //usuario elimnou um registro
            MessagingCenter.Subscribe<Application, Models.AgendaModel>(this, "DeleteDados", (sender, arg) =>
            {
                //atualiza dados no banco de dados local
                AgendaTable.EliminaRegistro(arg.Id);

                //atualiza lista
                MostraDados(null);
            });
        }

        async void Agenda_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //obtem item selecionado
            AgendaModel item = (AgendaModel)e.Item;
            if (item == null)
                return;

            //abrir view para edição do item selecionado
            var telaInicial = Application.Current.MainPage as Views.MasterDetailPrincipal;
            await telaInicial.PushAsync(new MntDados(item));
        }

        public void MostraDados(object texto)
        {
            List<AgendaModel> lista = AgendaTable.GetTelefones();
            if (lista == null)
                viewModel.Agendas = new ObservableCollection<AgendaModel>();
            else
            {
                if (texto != null)
                {
                    viewModel.Agendas = new ObservableCollection<AgendaModel>(lista.Where(x => x.Nome.ToLower().Contains(texto.ToString().ToLower())));
                }
                else
                {
                    viewModel.Agendas = new ObservableCollection<AgendaModel>(lista);
                }
            }
            viewModel.InformaAlteracao("Agendas");
            viewModel.InformaAlteracao("Nome");
            viewModel.InformaAlteracao("Telefone");
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var texto = AgendaSearchBar.Text;
            MostraDados(texto);
        }
    }

    public class AgendaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void InformaAlteracao(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        public ObservableCollection<AgendaModel> Agendas { get; set; }

        public ICommand IncluirCommand { protected set; get; }
        public ICommand CompartilharCommand { protected set; get; }

        public AgendaViewModel()
        {
            this.IncluirCommand = new Command(async () =>
            {
                //Incluir nova entrada na agenda
                var telaInicial = Application.Current.MainPage as Views.MasterDetailPrincipal;
                await telaInicial.PushAsync(new MntDados());
            });

            this.CompartilharCommand = new Command(async () => 
            {
                //Compartilhar dados da agenda
                string dados = string.Empty;
                foreach (AgendaModel a in Agendas)
                {
                    dados += a.Nome + " - " + a.Telefone + '\n';
                }

                //Chamar tela de compartilhamento (específica para cada plataforma)
                await DependencyService.Get<Interfaces.IDeviceSpecific>().CompartilharDados("Agenda Telefônica", dados);
            });
        }
    }
}