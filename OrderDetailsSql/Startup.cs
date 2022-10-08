using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderDetailsSql.Startup))]
namespace OrderDetailsSql
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
