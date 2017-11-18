using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Agenda
{
    public class OAuthProviderSetting
    {
        public string ClientId { get; private set; }
        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string RequestTokenUrl { get; private set; }
        public string AcessTokenUrl { get; private set; }
        public string AuthorizeUrl { get; private set; }
        public string CallbackUrl { get; private set; }

        public OAuth2Authenticator LoginWithProvider()
        {
            OAuth2Authenticator auth = new OAuth2Authenticator(
                clientId: "565425677125377",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            return auth;
        }

        //API request to get Profile Information from Facebook
        public async Task<bool> RequestLoginData(Account account)
        {
            bool bAuth = false;
            var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,name,email"),
                null, account);
            var response = await request.GetResponseAsync();
            var obj = JObject.Parse(response.GetResponseText());

            string token = account.Properties["access_token"].ToString();

            var expiresIn = Convert.ToDouble(account.Properties["expires_in"]);
            DateTime dtExpiry = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

            string idAut = obj["id"].ToString().Replace("\"", "");
            string nome = obj["name"].ToString().Replace("\"", "");

            string email = string.Empty;
            if(obj["email"] != null)
                email = obj["email"].ToString().Replace("\"", "");

            //envia mensagem para tela de login, informando se usuário autenticou-se
            MessagingCenter.Send<Application, bool>(App.Current, "Authentication", bAuth);

            return true;
        }

        async Task<string> fnDownloadString(string strUri)
        {
            var webclient = new HttpClient();
            string strResultData;
            try
            {
                strResultData = await webclient.GetStringAsync(new Uri(strUri));
            }
            catch
            {
                strResultData = String.Empty;
            }
            finally
            {
                if (webclient != null)
                {
                    webclient.Dispose();
                    webclient = null;
                }
            }
            return strResultData;
        }
    }
}
