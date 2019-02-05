using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using NotifSync.Backend.Utils;

namespace NotifSync.Backend.Server
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
        }
    }
}