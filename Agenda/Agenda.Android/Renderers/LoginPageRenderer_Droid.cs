using Android.App;
using Agenda.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Agenda.Views.LoginExternal), typeof(LoginPageRenderer_Droid))]
namespace Agenda.Droid.Renderers
{
    class LoginPageRenderer_Droid : PageRenderer
    {
        bool showLogin = true;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = this.Context as Activity;
            if (showLogin)
            {
                showLogin = false;

                //Classe OauthProviderSetting contém implementação do Oauth
                OAuthProviderSetting oauth = new OAuthProviderSetting();

                var auth = oauth.LoginWithProvider();

                //permite que usuário cancele a autenticação
                auth.AllowCancel = true;

                //Após login com facebook tiver sido completado
                auth.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated == false)
                        return;

                    //Obtém dados de autenticação do usuário
                    await oauth.RequestLoginData(eventArgs.Account);
                    var telaInicial = new Views.MasterDetailPrincipal();
                    App.MyApp.MainPage = telaInicial;
                };

                //inicia tela de login via Facebook
                activity.StartActivity((Android.Content.Intent)auth.GetUI(activity));
            }
        }
    }
}