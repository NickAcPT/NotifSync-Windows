using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Nancy.Hosting.Self;
using NotifSync.Backend;

namespace NotifSync.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public NancyHost NancyHost { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            SharedObjects.Instance.RandomMethod();
            RandomClass.Hello();

            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };

            var uri = new Uri("http://localhost:11785");
            NancyHost = new NancyHost(hostConfigs, uri);
            NancyHost?.Start();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            NancyHost?.Dispose();
            base.OnExit(e);
        }
    }
}
