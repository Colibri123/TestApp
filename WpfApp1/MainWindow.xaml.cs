
using Nevron.Nov.UI;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
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
        DataSet dataSet = new DataSet();
        SQLRequest SQLRequest = new SQLRequest();
        bool All = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public string[] editText(string text) 
        {
            string[] Name = null;
            char[] delimiterChars = { ' ', ',', ':', '/', '=', '&' };
            
                string g = text;
                g = string.Join(" ", g.Split(' ')
                .Select(x =>
                    string.Concat(
                        x.Length >= 1 ? x.Substring(0, 1).ToUpper() : "",
                        x.Length > 1 ? x.Substring(1, x.Length - 1).ToLower() : "")
                    )); ;
                return Name = g.Split(delimiterChars);            
            
        }

        private void updateDataGrid()
        {            
           
            grid.Dispatcher.Invoke(() => 
            {
                grid.ItemsSource = SQLRequest.Request(@$"SELECT FirmDS.Name,
                                    		FirmDS.Inn,
                                    		FirmDS.Phone,
                                    		FirmDS.EMAIL,
                                    		FirmDS.Reserve,
                                    		FirmDS.VipSign,
                                    		FirmDS.PostAddress,
                                    		FirmDS.JurAddress,
                                    		FirmDS.LicNumber,
                                    		FirmDS.BeginDate,
                                    		CityDS.Name,
                                    		CityDS.BeginDate,
                                    		UserDS.UserName,
                                    		RegionDS.RegionName,
                                    		StatusOfLocalsDS.StatusOfLocalsName,
                                    		OwnerShipsDS.OwnerShipsName
                                    FROM CityDS INNER JOIN
                                    		FirmDS ON CityDS.CityID = FirmDS.Post_city_iD 
                                    		{(All ? "" : $"AND FirmDS.Name IN('{string.Join("','", editText(FirmName.Text))}') AND")} {(All ? "" : $" FirmDS.JurAddress IN('{string.Join("','", editText(JurAddress.Text))}') AND")} {(All ? "" : $" FirmDS.PostAddress IN('{string.Join("','", editText(PostAddress.Text))}')")} INNER JOIN
                                    		RegionDS ON CityDS.RegionID = RegionDS.RegionID INNER JOIN
                                    		UserDS ON FirmDS.UserID = UserDS.UserID INNER JOIN
                                    		StatusOfLocalsDS ON CityDS.StatusOfLocalsID = StatusOfLocalsDS.StatusOfLocalsID INNER JOIN
                                    		OwnerShipsDS ON FirmDS.OwnerShipsID = OwnerShipsDS.OwnerShipsID").Tables[0].DefaultView;

            });
           

        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void CityName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            new Thread(updateDataGrid).Start();
        }

        private void allData_Checked(object sender, RoutedEventArgs e)
        {
            All = true; 
            new Thread(updateDataGrid).Start();
        }
    }
}
