using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MntDados : ContentPage
    {
        private DadosViewModel viewModel;
        public Models.AgendaModel dadosAgenda;

        public MntDados(Models.AgendaModel agenda = null)
        {
            //salva dados corrente
            dadosAgenda = agenda;

            InitializeComponent();
            viewModel = new DadosViewModel(this);
            this.BindingContext = viewModel;

            //mostra dados da agenda corrente
            if (agenda != null)
            {
                viewModel.Nome = agenda.Nome;
                viewModel.Telefone = agenda.Telefone;
                viewModel.Imagem = agenda.Imagem;

                viewModel.InformaAlteracao("Nome");
                viewModel.InformaAlteracao("Telefone");
                viewModel.InformaAlteracao("Imagem");
            }
            else
            {
                viewModel.Imagem = "profile.png";
                viewModel.InformaAlteracao("Imagem");
            }
        }

        public bool DadosOk()
        {
            if (string.IsNullOrEmpty(viewModel.Nome))
            {
                DisplayAlert("Erro", "Nome não foi informado", "Ok");
                txtNome.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(viewModel.Telefone))
            {
                DisplayAlert("Erro", "Telefone não foi informado", "Ok");
                txtFone.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(viewModel.Imagem))
            {
                DisplayAlert("Erro", "Imagem não foi informada", "Ok");
                txtFone.Focus();
                return false;
            }

            return true;
        }

        private async void tirarFoto_clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Nenhuma Câmera", "Nenhuma Câmera disponível.", "OK");
                return;
            }

            var armazenamento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                Name = "MinhaFoto.jpg"
            };
            var foto = await CrossMedia.Current.TakePhotoAsync(armazenamento);
            viewModel.Imagem = foto.Path;
            viewModel.InformaAlteracao("Imagem");
            if (foto == null)
                return;

            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = foto.GetStream();
                foto.Dispose();
                return stream;
            });
        }
    }

    public class DadosViewModel : INotifyPropertyChanged
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Imagem { get; set; }

        public MntDados ParentPage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OkCommand { protected set; get; }
        public ICommand DeleteCommand { protected set; get; }
        public ICommand shareCommand { protected set; get; }

        public void InformaAlteracao(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        public DadosViewModel(MntDados pai)
        {
            ParentPage = pai;
            this.OkCommand = new Command(async () =>
            {
                //verifica se todos os campos foram informados
                if (ParentPage.DadosOk())
                {
                    //atualizar lista de eventos da aplicação, avisando janela pai que dados foram alterados
                    Models.AgendaModel novo = new Models.AgendaModel(0, Nome, Telefone, Imagem);
                    if (ParentPage.dadosAgenda != null)
                        novo.Id = ParentPage.dadosAgenda.Id;
                    MessagingCenter.Send<Application, Models.AgendaModel>(App.Current, "MntDados", novo);

                    //encerrar tela
                    var telaInicial = Application.Current.MainPage as Views.MasterDetailPrincipal;
                    await telaInicial.PopAsync();
                }
            });

            this.DeleteCommand = new Command(async() =>
            {
                if (ParentPage.dadosAgenda == null)
                    await ParentPage.DisplayAlert("Erro", "Dados não podem ser eliminados", "Ok");
                else
                {
                    var respota = await ParentPage.DisplayAlert("Elimina Registro", "Confirma eliminação do registro?", "Sim", "Não");
                    if (respota)
                    {
                        MessagingCenter.Send<Application, Models.AgendaModel>(App.Current, "DeleteDados", ParentPage.dadosAgenda);

                        //encerrar tela
                        var telaInicial = Application.Current.MainPage as Views.MasterDetailPrincipal;
                        await telaInicial.PopAsync();
                    }
                }
            });

            this.shareCommand = new Command(async () =>
            {
                if (ParentPage.dadosAgenda != null)
                {
                    string dados = ParentPage.dadosAgenda.Nome + " - " + ParentPage.dadosAgenda.Telefone;
                    await DependencyService.Get<Interfaces.IDeviceSpecific>().CompartilharDados("Agenda Telefônica", dados);
                }
                else
                {
                    await ParentPage.DisplayAlert("Alerta", "Não foi possível compartilhar dados", "Ok");
                }
            });
        }
    }
}