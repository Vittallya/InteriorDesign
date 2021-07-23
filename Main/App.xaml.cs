using BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Locator.InitServices();
            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            (Locator.Services.GetService(typeof(DbContextLoader)) as DbContextLoader)?.Abort();
            base.OnExit(e);
        }
    }
}
