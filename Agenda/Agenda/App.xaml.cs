using Agenda.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agenda.BDLocal;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Agenda
{
    public partial class App : Application
    {
        //banco de dados local
        private static BancoLocal localDataBase;
        private static Application myApp;

        public static Application MyApp
        {
            get { return myApp; }
        }

        public static BancoLocal BDLocal
        {
            get { return localDataBase; }
        }

        public App()
        {
            InitializeComponent();

            //Connecta ao banco de dados local {cria BD se ele ainda não existir}
            localDataBase = new BancoLocal();

            //página inicial da aplicação(com barra de navegação) e login no facebook
            /*Views.Login pagInicial = new Views.Login();
            MainPage = new NavigationPage(pagInicial)
            {
                BarBackgroundColor = Color.DarkSeaGreen,
                BarTextColor = Color.White
            };

            NavigationPage.SetBackButtonTitle(MainPage, "");
            NavigationPage.SetHasBackButton(MainPage, true);
            myApp = this;*/

            //Página principal sem o login do facebook
            var telaInicial = new Views.MasterDetailPrincipal();
            Application.Current.MainPage = telaInicial;
        }
    }
}
