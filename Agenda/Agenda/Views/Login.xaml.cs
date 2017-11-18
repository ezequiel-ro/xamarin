using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, string.Empty);
            NavigationPage.SetHasNavigationBar(this, false);

            //espera o usuário fazer a autenticação
            MessagingCenter.Subscribe<Application, bool>(this, "Authentication", (sender, arg) => 
            {
                if (arg)
                {
                    App.Current.MainPage.Navigation.PushAsync(new PaginaInicial());
                }
            });
        }

        private async void LoginFacebook(object sender, EventArgs e)
        {
            //mostra tela de autenticação via Facebook
            await Navigation.PushModalAsync(new LoginExternal());
        }
    }
}