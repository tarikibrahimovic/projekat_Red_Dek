using projekat_Red_Dek.ViewModels;
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

namespace projekat_Red_Dek.Views
{
    /// <summary>
    /// Interaction logic for RedMain.xaml
    /// </summary>
    public partial class RedMain : Window
    {
        public RedMain()
        {
            InitializeComponent();
            DataContext = this.Resources["vm"];
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var vm = this.DataContext as RedVM;
            Window mywindow = sender as Window;
            vm.postaviDimenzije(mywindow.ActualHeight, mywindow.ActualWidth);
        }
    }
}
