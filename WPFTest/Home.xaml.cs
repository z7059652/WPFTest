using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFTest.ExtendMethod;
using WPFTest.Model;
namespace WPFTest
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        FileService FileSrv = new FileService();
        public ObservableCollection<Program> ObLocalList = null;

        public IList<Program> LocalList = new List<Program>();
        public Home()
        {
            InitializeComponent();
            AProgram local = new LocalProgram();
            ObLocalList = new ObservableCollection<Program>(LocalList);
            this.IconList.ItemsSource = ObLocalList;
        }
        private void Double_Click(object sender, MouseButtonEventArgs e)
        {
            Program p = IconList.SelectedItem as Program;
            LanuchProcess lp = new LanuchProcess();
            if(p != null)
                lp.Start(p.Path);
        }

        private void BeginMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwd = new MainWindow(LocalList);
            mainwd.pwnd = this;
            mainwd.ShowDialog();
            this.IconList.ItemsSource = ObLocalList;
            LocalList = ObLocalList;
        }
    }
}
