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
using SQLite;
using System.IO;
using Agenda.Droid.Implementacao;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnection_Droid))]
namespace Agenda.Droid.Implementacao
{
    public class SQLiteConnection_Droid : ISQLiteConnection
    {
        public SQLiteConnection DBConnection()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "agenda.db3");
            return new SQLiteConnection(path);
        }
    }
}