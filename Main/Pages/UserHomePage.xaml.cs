using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserHomePage.xaml
    /// </summary>
    public partial class UserHomePage : Page
    {
        public UserHomePage()
        {
            InitializeComponent();


		}


        private void Storyboard_Completed_1(object sender, EventArgs e)
        {

        }

        private async void tbMain_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            

        }

        private void tbMain_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool vis)
            {
                
                var elem = sender as FrameworkElement;

                DoubleAnimation anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.9));

                if (!vis)
                {
                    anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
                    
                }

                elem.BeginAnimation(OpacityProperty, anim);

            }
        }

        private async void Rectangle_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool vis && !vis)
            { 

                var elem = sender as FrameworkElement;

                DoubleAnimation anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));

                elem.BeginAnimation(OpacityProperty, anim);
                await Task.Delay(500);
                elem.Visibility = Visibility.Collapsed;
                    
            }
        }
    }
}
