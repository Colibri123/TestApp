
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLRequest SQLRequest = new SQLRequest();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void updateDataGrid()
        {
            DataSet dataSet = new DataSet();
            dataSet = SQLRequest.Request("select * from FirmDS");
            grid.Dispatcher.Invoke(() => 
            {
                grid.ItemsSource = dataSet.Tables[0].DefaultView;

            });
           

        }

        private void setConnection()
        {
            var connectString = "Provider=chtpz/w@garnet;Data Source=chtpz/w@garnet;Password=secret;User ID=scott";
            var con = new OleDbConnection(connectString);
            con.Open();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(updateDataGrid).Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
