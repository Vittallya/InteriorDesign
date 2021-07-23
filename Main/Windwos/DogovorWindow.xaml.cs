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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Main.Windows
{
    /// <summary>
    /// Логика взаимодействия для DogovorWindow.xaml
    /// </summary>
    public partial class DogovorWindow : Window
    {
        public DogovorWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            p.PrintDocument(((IDocumentPaginatorSource)this.viewer.Document).DocumentPaginator, "Договор оказания услуг");
            p.ShowDialog();

        }
    }
}
