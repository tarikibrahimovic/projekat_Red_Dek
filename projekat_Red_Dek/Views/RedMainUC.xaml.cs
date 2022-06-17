using projekat_Red_Dek.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace projekat_Red_Dek.Views
{
    /// <summary>
    /// Interaction logic for RedMainUC.xaml
    /// </summary>
    public partial class RedMainUC : UserControl
    {
        public RedMainUC()
        {
            InitializeComponent();
            DataContext = this.Resources["vm"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            (win.DataContext as MainVM).SelectedViewModel = new DekVM();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var vm = this.DataContext as RedVM;
            Window mywindow = Window.GetWindow(this);
            vm.postaviDimenzije(mywindow.ActualHeight, mywindow.ActualWidth);
        }        
    }
}
