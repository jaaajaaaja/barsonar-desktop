using barsonar_desktop.services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace barsonar_desktop.view
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly ApiService _apiService = new ApiService();

        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void bntMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            txtError.Visibility = Visibility.Collapsed;
            txtLoading.Visibility = Visibility.Visible;
            btnLogin.IsEnabled = false;

            try
            {
                var email = txtBox_Email.Text.Trim();
                var password = pwdBox_Pass.Password;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Kérlek add meg az email címed és a jelszavad!");
                    return;
                }

                var user = await _apiService.LoginAsync(email, password);

                if (user.Role != "admin")
                {
                    ShowError("Csak admin felhasználók jelentkezhetnek be!");
                    return;
                }

                var mainWindow = new MainWindow(_apiService, user.Username);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404") || ex.Message.Contains("NotFound"))
                    ShowError("A felhasználó nem található!");
                else if (ex.Message.Contains("401") || ex.Message.Contains("Unauthorized"))
                    ShowError("Hibás email cím vagy jelszó!");
                else
                    ShowError("Hiba történt a bejelentkezés során!");
            }
            finally
            {
                txtLoading.Visibility = Visibility.Collapsed;
                btnLogin.IsEnabled = true;
            }
        }

        private void ShowError(string message)
        {
            txtError.Text = message;
            txtError.Visibility = Visibility.Visible;
        }
    }
}
