using compras.Helper;
using compras.View;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace compras
{
    public partial class App : Application
    {

        static SQLiteDatabaseHelper database;

        public static SQLiteDatabaseHelper Database
        {
            get 
            { 
                if(database == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"arquivo.db3");

                    database = new SQLiteDatabaseHelper(path);
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new Listagem());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
