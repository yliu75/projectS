using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shippingCodefirstTest.Startup))]
namespace shippingCodefirstTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
