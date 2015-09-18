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
        public Home pwnd = null;
        AProgram local = new LocalProgram();
        IList<Program> ReadyCurrentSelected = new List<Program>();
        IList<Program> LocalCurrentSelected = new List<Program>();
        public MainWindow(IList<Program> LoList)
        {
           InitializeComponent();

           LocalList = (List<Program>)local.LoadProgram(LoList);
           ObLocalList = new ObservableCollection<Program>(LocalList);
           LocalProgram.ItemsSource = ObLocalList;
           
           ProList = LoList;
           ObReadyList = new ObservableCollection<Program>(LoList);
           ReadyProgram.ItemsSource = ObReadyList;
        }

        private void Local2Ready_Click(object sender, RoutedEventArgs e)
        {
            HandleProgramService.INST.RefreshSelected(ref LocalCurrentSelected, LocalProgram.SelectedItems);
            HandleProgramService.INST.MoveTo(ObLocalList, ObReadyList, LocalCurrentSelected);
            ProList = ObReadyList;
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
            Home a = new Home();
            a.ShowDialog();
            HandleProgramService.INST.RefreshSelected(ref ReadyCurrentSelected, ReadyProgram.SelectedItems);            
            LanuchProcess lp = new LanuchProcess();
            lp.Start(ReadyCurrentSelected);
        }
        protected override void OnClosed(EventArgs e)
        {
            pwnd.ObLocalList = ObReadyList;
            base.OnClosed(e);
        }
    }
}
