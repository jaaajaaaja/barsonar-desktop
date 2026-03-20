using barsonar_desktop.classes;
using barsonar_desktop.services;
using barsonar_desktop.view;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            if (loadingOverlay != null)
                loadingOverlay.Visibility = Visibility.Visible;

            try
            {
                var photos = await _api.GetAllPhotosAsync();
                photosPanel.Items.Clear();

                foreach (var photo in photos)
                {
                    photosPanel.Items.Add(BuildPhotoCard(photo));
                }

                if (photos.Count == 0)
                    photosPanel.Items.Add(EmptyMessage("Nincsenek fotók."));
            }
            catch (Exception ex)
            {
                photosPanel.Items.Clear();
                photosPanel.Items.Add(EmptyMessage($"Hiba: {ex.Message}"));
            }
            finally
            {
                if (loadingOverlay != null)
                    loadingOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private UIElement BuildPhotoCard(Photo photo)
        {
            var card = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222240")),
                CornerRadius = new CornerRadius(12),
                Width = 240,
                Margin = new Thickness(0, 0, 12, 12),
                Padding = new Thickness(0)
            };

            var stack = new StackPanel();

            var img = new Image
            {
                Height = 160,
                Stretch = Stretch.UniformToFill,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            img.Clip = new RectangleGeometry(new System.Windows.Rect(0, 0, 240, 160))
            {
                RadiusX = 12,
                RadiusY = 12
            };

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_api.GetImageUrl(photo.Location));
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                img.Source = bitmap;
            }
            catch
            {
                img.Source = null;
            }
            stack.Children.Add(img);

            var info = new StackPanel { Margin = new Thickness(12, 10, 12, 12) };

            var badge = CreateStatusBadge(photo.Approved);
            info.Children.Add(badge);

            info.Children.Add(new TextBlock
            {
                Text = $"ID: {photo.Id}  •  Típus: {photo.Type}",
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#888")),
                FontSize = 11,
                Margin = new Thickness(0, 6, 0, 0),
                TextTrimming = TextTrimming.CharacterEllipsis
            });

            info.Children.Add(new TextBlock
            {
                Text = $"Felhaszáló: {photo.UserID}  •  Hely: {photo.PlaceID}",
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666")),
                FontSize = 11,
                Margin = new Thickness(0, 2, 0, 0)
            });

            var btnRow = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 8, 0, 0)
            };

            if (!photo.Approved)
            {
                var approveBtn = CreateStyledButton("Jóváhagyás", "#2ECC71", "#27AE60");
                approveBtn.Click += async (s, e) =>
                {
                    try
                    {
                        await _api.ApprovePhotoAsync(photo.Id);
                        await LoadPhotos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                };
                btnRow.Children.Add(approveBtn);
            }

            var deleteBtn = CreateStyledButton("Törlés", "#E74C3C", "#C0392B");
            deleteBtn.Margin = new Thickness(6, 0, 0, 0);
            deleteBtn.Click += async (s, e) =>
            {
                var result = MessageBox.Show("Biztosan törölni szeretnéd?", "Megerősítés",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _api.DeletePhotoAsync(photo.Id);
                        await LoadPhotos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            };
            btnRow.Children.Add(deleteBtn);

            info.Children.Add(btnRow);
            stack.Children.Add(info);
            card.Child = stack;
            return card;
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

        //HELPERS

        private Border CreateStatusBadge(bool approved)
        {
            var badge = new Border
            {
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(8, 3, 8, 3),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var text = new TextBlock { FontSize = 11, FontWeight = FontWeights.SemiBold };

            if (approved)
            {
                badge.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A3A1A"));
                text.Text = "✓ Jóváhagyva";
                text.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ECC71"));
            }
            else
            {
                badge.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3A2000"));
                text.Text = "⏳ Függőben";
                text.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEC428"));
            }

            badge.Child = text;
            return badge;
        }

        private Button CreateStyledButton(string content, string bgColor, string hoverColor)
        {
            var btn = new Button
            {
                Content = content,
                Foreground = Brushes.White,
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                Cursor = Cursors.Hand,
                Padding = new Thickness(14, 6, 14, 6)
            };

            var bg = (Color)ColorConverter.ConvertFromString(bgColor);
            var hover = (Color)ColorConverter.ConvertFromString(hoverColor);

            var template = new ControlTemplate(typeof(Button));
            var borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, new SolidColorBrush(bg));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(8));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(14, 6, 14, 6));
            borderFactory.Name = "btnBorder";

            var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            borderFactory.AppendChild(contentPresenter);

            template.VisualTree = borderFactory;

            var hoverTrigger = new Trigger { Property = Button.IsMouseOverProperty, Value = true };
            hoverTrigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush(hover), "btnBorder"));
            template.Triggers.Add(hoverTrigger);

            btn.Template = template;
            return btn;
        }

        private UIElement EmptyMessage(string text)
        {
            return new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666")),
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 40, 0, 0)
            };
        }
    }
}