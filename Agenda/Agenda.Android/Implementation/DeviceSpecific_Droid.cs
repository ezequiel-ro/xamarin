using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Agenda.Interfaces;
using Agenda.Droid.Implementation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Uri = Android.Net.Uri;
using Plugin.Media;
using Plugin.Media.Abstractions;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceSpecific_Droid))]
namespace Agenda.Droid.Implementation
{
    class DeviceSpecific_Droid : IDeviceSpecific
    {
        public async Task<bool> CompartilharDados(string titulo, string dados)
        {
            try {
                var intent = new Intent(Intent.ActionSend);
                intent.SetType("text/plain");
                intent.PutExtra(Intent.ExtraSubject, titulo);
                intent.PutExtra(Intent.ExtraText, dados);

                var chooserIntent = Intent.CreateChooser(intent, "Compartilhamento Dados da Agenda");
                chooserIntent.SetFlags(ActivityFlags.ClearTop);
                chooserIntent.SetFlags(ActivityFlags.NewTask);
                Xamarin.Forms.Forms.Context.StartActivity(chooserIntent);

                bool ret = await Task.FromResult(true);
                return ret;
            } catch (Exception ex) {
                string msg = "Dados não podem ser compartilhados! \n[Erro: " + ex.Message + "]";
                Toast.MakeText(Forms.Context, msg, ToastLength.Long).Show();
                return false;
            }
        }

        public void CloseApplication()
        {
            Process.KillProcess(Process.MyPid());
        }

        public void fazerLigacao(string numeroTelefone)
        {
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Uri.Parse("tel:" + numeroTelefone));
            Forms.Context.StartActivity(intent);
        }        
    }
}