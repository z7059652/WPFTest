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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTest.Model;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
//        List<Program> ReadyList = new List<Program>();
        List<Program> LocalList = new List<Program>();
        ObservableCollection<Program> ReadyList = new ObservableCollection<Program>();
        Program CurrentSelected = null;
        public MainWindow()
        {
           InitializeComponent();
           List<Program> ProList = new List<Program>();
           ProList.Add(new Program(false, "SKYPE", "C:\\Program Files (x86)\\Skype\\Phone\\Skype.exe"));
           ReadyList = new ObservableCollection<Program>(ProList);
           ReadyProgram.ItemsSource = ReadyList;
        }


        private void RowSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentSelected = ReadyProgram.SelectedItem as Program;
            MessageBox.Show(CurrentSelected.Name);
        }
        
 
    }
}
