using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Agenda.Interfaces;
using SQLite;
using System.IO;
using Agenda.iOS.Inplementation;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnection_Ios))]
namespace Agenda.iOS.Inplementation
{
    public class SQLiteConnection_Ios: ISQLiteConnection
    {
        public SQLiteConnection DBConnection()
        {
            string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, "agenda.db3");
            return new SQLiteConnection(path);
        }
    }
}