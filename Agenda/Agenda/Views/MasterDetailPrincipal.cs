using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Agenda.Views;

namespace Agenda.Views
{
    public class MasterDetailPrincipal : MasterDetailPage
    {
        public NavigationPage navegacao = null;

        public MasterDetailPrincipal()
        {
            navegacao = new NavigationPage(new Views.PaginaInicial());
            Detail = navegacao;
            Master = new Views.Menu();
        }

        public async Task PushAsync(Page pagina)
        {
            IsPresented = false;
            await navegacao.PushAsync(pagina);
        }

        public async Task PopAsync()
        {
            await navegacao.PopAsync();
        }
    }
}
