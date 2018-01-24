using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImmediateRefreshWithLongDBQuery.Startup))]
namespace ImmediateRefreshWithLongDBQuery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
