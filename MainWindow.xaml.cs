using BWI_Hardwareverteilung.Knapsack;
using BWI_Hardwareverteilung.Models;
using BWI_Hardwareverteilung.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BWI_Hardwareverteilung
{
    /// <summary>
    /// GUI-Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Diese Klasse ist die Schnittstelle zum "Back-End" für die GUI
        /// </summary>
        private MainViewModel windowDataContext;

        public MainWindow()
        {
            InitializeComponent();
            this.windowDataContext = (MainViewModel)this.DataContext;
        }

        /// <summary>
        /// Übernimmt einen Klick auf den Button "ExportData"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportData_Click(object sender, RoutedEventArgs e)
        {
            windowDataContext.ExportData();
        }

        /// <summary>
        /// Übernimmt einen Klick auf den Button "ImpotData"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportData_Click(object sender, RoutedEventArgs e)
        {
            windowDataContext.ImportData();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Greedy test = new Greedy(windowDataContext.TransportableObjectsList.ToArray(), windowDataContext.TransporterList.ToArray());
           
        }

        private void Partition_Click(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new Greedy(windowDataContext.TransportableObjectsList.ToArray(), windowDataContext.TransporterList.ToArray());
            foreach (Transporter t in windowDataContext.TransporterList)
            {
                t.NotifyPropertyChanged("Name");
                t.NotifyPropertyChanged("WeightOfDriver");
                t.NotifyPropertyChanged("MaxCapacity");
                t.NotifyPropertyChanged("ObjectList");
                t.NotifyPropertyChanged("ResultText");
                t.NotifyPropertyChanged("resultScore");
            }
            windowDataContext.NotifyPropertyChanged("ResultScore");
        }
    }
}
