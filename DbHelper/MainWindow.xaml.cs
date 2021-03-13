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

        ICollection<User> _users;

        async void Init()
        {
            await Task.Run(() => context = new AllDbContext());
            UpdateLists();
            //RoleCombo.ItemsSource = Enum.GetValues(typeof(Role));
        }

        async void UpdateLists()
        {
            //await context.Users.LoadAsync();
            //_users = context.Users.ToList();
            grid.ItemsSource = _users;
        }


        string file;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(tb.Text) &&
            //    RoleCombo.SelectedItem != null &&
            //    !string.IsNullOrEmpty(tbLogin.Text) &&
            //    !string.IsNullOrEmpty(tbPass.Text))
            //{
               
                
            //    context.Users.Add(new User
            //    {
            //        Login = tbLogin.Text,
            //        Password = tbPass.Text,
            //        Name = tb.Text,
            //        Role = (DAL.Models.Role)Enum.Parse(typeof(DAL.Models.Role), RoleCombo.SelectedItem.ToString()),
            //    });
            //    await context.SaveChangesAsync();


            //    UpdateLists();
            //    MessageBox.Show("Добавлен");

            //}


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
            //if (Combo.SelectedItem != null && Combo.SelectedItem is User user)
            //{
            //    BitmapImage image = new BitmapImage();

            //    using (var sm = new MemoryStream(user.Image))
            //    {
            //        sm.Position = 0;
            //        image.BeginInit();
            //        image.StreamSource = sm;
            //        image.CacheOption = BitmapCacheOption.OnLoad;
            //        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            //        image.UriSource = null;
            //        image.EndInit();
            //    }
            //    tbLoginView.Text = user.Email;
            //    tbPassView.Text = user.Password;

            //    img.Source = image;


            //}


        }


        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var users = grid.ItemsSource;

            foreach(var user in users)
            {
                context.Entry(user).State = EntityState.Modified;
            }



            await context.SaveChangesAsync();

            MessageBox.Show("Обновлено");
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(grid.SelectedItem != null && grid.SelectedItem is User user)
            {
                //context.Users.Remove(user);
                //await context.SaveChangesAsync();
                //UpdateLists();
                //MessageBox.Show("Удалено");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UpdateLists();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Execute(SqlInstructions.ExecuteServices);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Execute(SqlInstructions.ExecuteStyles);
        }

        async void Execute(Func<Task> func)
        {
            IsEnabled = false;
            await func?.Invoke();
            IsEnabled = true;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Execute(SqlInstructions.ExecuteEmployee);
        }
    }
}
