using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agenda.Interfaces;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel();
        }

        public class MenuViewModel
        {
            public ICommand MenuTapped { get; set; }
            public MenuViewModel()
            {
                this.MenuTapped = new Command<string>(async (id) =>
                {
                    switch (id)
                    {
                        case "novo":
                            var telaInicial = Application.Current.MainPage as Views.MasterDetailPrincipal;
                            await telaInicial.PushAsync(new MntDados());
                            break;
                        case "sair":
                            DependencyService.Get<IDeviceSpecific>().CloseApplication();
                            break;
                    }
                });
            }
        }
    }
}