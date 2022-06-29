using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Text_Splitter.Startup))]
namespace Text_Splitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
