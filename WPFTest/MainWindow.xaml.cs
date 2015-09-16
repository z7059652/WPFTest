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
        IList<Program> ProList = null;
        IList<Program> LocalList = null;
        ObservableCollection<Program> ObReadyList = null;
        ObservableCollection<Program> ObLocalList = null;

        AProgram local = new LocalProgram();
        AProgram ready = new ReadyProgram();
        IList<Program> ReadyCurrentSelected = new List<Program>();
        IList<Program> LocalCurrentSelected = new List<Program>();
        public MainWindow()
        {
           InitializeComponent();
           LocalList = (List<Program>)local.LoadProgram();
           ObLocalList = new ObservableCollection<Program>(LocalList);
           LocalProgram.ItemsSource = ObLocalList;

           ProList = ready.LoadProgram();
           ObReadyList = new ObservableCollection<Program>(ProList);
           ReadyProgram.ItemsSource = ObReadyList;
        }

        private void Local2Ready_Click(object sender, RoutedEventArgs e)
        {
            HandleProgramService.INST.RefreshSelected(ref LocalCurrentSelected, LocalProgram.SelectedItems);
            HandleProgramService.INST.MoveTo(ObLocalList, ObReadyList, LocalCurrentSelected);
        }
        private void Ready2Local_Click(object sender, RoutedEventArgs e)
        {
            HandleProgramService.INST.RefreshSelected(ref ReadyCurrentSelected, ReadyProgram.SelectedItems);
            HandleProgramService.INST.MoveTo(ObReadyList,ObLocalList,  ReadyCurrentSelected);
        }

        private void LocalRowSelectChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void ReadyRowSelectChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            HandleProgramService.INST.RefreshSelected(ref ReadyCurrentSelected, ReadyProgram.SelectedItems);            
            LanuchProcess lp = new LanuchProcess();
            lp.Start(ReadyCurrentSelected);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        } 
    }
}
