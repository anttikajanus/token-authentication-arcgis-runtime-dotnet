using Prism.Ioc;
using System.ComponentModel;
using System.Windows;

namespace TokenAuthentication.Example
{
    public partial class App 
    {
        protected override Window CreateShell()
        {
            var shell = Container.Resolve<MainWindow>();
            return shell;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
        }
    }
}
