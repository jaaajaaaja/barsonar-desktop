using barsonar_desktop.classes;
using barsonar_desktop.services;
using barsonar_desktop.view;
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
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPhotos();
        }

        //WINDOW CONTROLLS

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginView();
            login.Show();
            this.Close();
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
            SetActiveNav(btnNavComments);
            tabControl.SelectedItem = tabComments;
            await LoadComments();
        }

        private async void btnNavNews_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav(btnNavNews);
            tabControl.SelectedItem = tabNews;
            await LoadNews();
        }

        private async void btnNavAllData_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav(btnNavAllData);
            tabControl.SelectedItem = tabAllData;
            await LoadAllData();
        }

        private async void btnNavStatistics_Click(object sender, RoutedEventArgs e)
        {
            SetActiveNav(btnNavStatistics);
            tabControl.SelectedItem = tabStatistics;
            await LoadStatistics();
        }

        private void SetActiveNav(Button activeButton)
        {
            var activeStyle = (Style)FindResource("SidebarButtonActive");
            var normalStyle = (Style)FindResource("SidebarButton");

            btnNavPhotos.Style = normalStyle;
            btnNavComments.Style = normalStyle;
            btnNavNews.Style = normalStyle;
            btnNavAllData.Style = normalStyle;
            btnNavStatistics.Style = normalStyle;
            activeButton.Style = activeStyle;
        }

        //REFRESH

        private async void btnRefreshPhotos_Click(object sender, RoutedEventArgs e) => await LoadPhotos();
        private async void btnRefreshComments_Click(object sender, RoutedEventArgs e) => await LoadComments();
        private async void btnRefreshNews_Click(object sender, RoutedEventArgs e) => await LoadNews();
        private async void btnRefreshAllData_Click(object sender, RoutedEventArgs e) => await LoadAllData();
        private async void btnRefreshStatistics_Click(object sender, RoutedEventArgs e) => await LoadStatistics();

        private async void cmbPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPeriod.SelectedItem is ComboBoxItem selected && selected.Tag != null)
            {
                if (Enum.TryParse<PeriodEnum>(selected.Tag.ToString(), true, out var period))
                {
                    await LoadStatistics(period);
                }
            }
        }

        //LOAD

        private async Task LoadPhotos()
        {

        }

        private UIElement BuildPhotoCard(Photo photo)
        {
            return null;
        }

        private async Task LoadComments()
        {

        }

        private UIElement BuildCommentCard(Comment comment)
        {
            return null;
        }

        private async Task LoadNews()
        {

        }
        private UIElement BuildNewsCard(News news)
        {
            return null;
        }

        private async Task LoadAllData()
        {

        }
        private async Task LoadStatistics(PeriodEnum period = PeriodEnum.MONTH, int? year = null)
        {

        }
    }
}