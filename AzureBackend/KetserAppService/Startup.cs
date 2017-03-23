using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KetserAppService.Startup))]

namespace KetserAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}