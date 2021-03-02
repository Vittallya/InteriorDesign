using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL;
using BL;
using DAL.Models;
using System.Data.Entity;

namespace DbHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AllDbContext context;

        public MainWindow()
        {
            InitializeComponent();
            Init();
            Closing += (a, b) => context.Dispose();
        }

        async void Init()
        {
            await Task.Run(() => context = new AllDbContext());
            UpdateLists();
            RoleCombo.ItemsSource = Enum.GetValues(typeof(Role));
        }

        async void UpdateLists()
        {
            await context.Users.LoadAsync();
            Combo.ItemsSource = context.Users.ToList();

        }


        string file;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb.Text) &&
                RoleCombo.SelectedItem != null &&
                !string.IsNullOrEmpty(tbLogin.Text) &&
                !string.IsNullOrEmpty(tbPass.Text) &&
                file != null)
            {
               
                using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    byte[] image = new byte[stream.Length];

                    await stream.ReadAsync(image, 0, (int)stream.Length);
                    context.Users.Add(new User
                    {
                        Image = image,
                        Login = tbLogin.Text,
                        Password = tbPass.Text,
                        Name = tb.Text,
                        Role = (DAL.Models.Role)Enum.Parse(typeof(DAL.Models.Role), RoleCombo.SelectedItem.ToString()),
                    });
                    await context.SaveChangesAsync();
                }

                UpdateLists();
            }

            
        }

        private void selectImage_Click(object sender, RoutedEventArgs e)
        {

            var service = new FileBrowserService();

            if(service.GetImageFile(out file))
            {
                tb4Path.Text = file;
            }
        }



        private void Look_Click(object sender, RoutedEventArgs e)
        {
            if (Combo.SelectedItem != null && Combo.SelectedItem is User user)
            {
                BitmapImage image = new BitmapImage();

                using (var sm = new MemoryStream(user.Image))
                {
                    sm.Position = 0;
                    image.BeginInit();
                    image.StreamSource = sm;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.UriSource = null;
                    image.EndInit();
                }
                tbLoginView.Text = user.Login;
                tbPassView.Text = user.Password;

                img.Source = image;


            }


        }
    }
}
