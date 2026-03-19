using barsonar_desktop.services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace barsonar_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiService _api;

        public MainWindow(ApiService api, string username)
        {
            InitializeComponent();
            _api = api;
            txtWelcome.Text = $"Üdv, {username}!";
            Loaded += MainWindow_Loaded;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPhotos();
        }

        //WINDOW CONTROLLS

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

        }

        //NAVIGATION

        private async void btnNavPhotos_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav(btnNavPhotos);
            tabControl.SelectedItem = tabPhotos;
            await LoadPhotos();
        }

        private async void btnNavComments_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnNavNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnNavAllData_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnNavStatistics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetActiveNav(Button activeButton)
        {

        }

        //REFRESH

        private async void btnRefreshPhotos_Click(object sender, RoutedEventArgs e) 
        { 

        }

        private async void btnRefreshComments_Click(object sender, RoutedEventArgs e) 
        {

        }

        private async void btnRefreshNews_Click(object sender, RoutedEventArgs e) 
        {

        }

        private async void btnRefreshAllData_Click(object sender, RoutedEventArgs e) 
        {

        }

        private async void btnRefreshStatistics_Click(object sender, RoutedEventArgs e) 
        {

        }

        private async void cmbPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}