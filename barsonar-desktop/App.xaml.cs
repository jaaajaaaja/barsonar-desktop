using DotNetEnv;
using System.Windows;

namespace barsonar_desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Env.Load();
        }
    }

}
