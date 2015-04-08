using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FvckYeahBubbleTea.Startup))]
namespace FvckYeahBubbleTea
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
