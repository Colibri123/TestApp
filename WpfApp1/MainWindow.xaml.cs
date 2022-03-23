using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using WpfApp1.Classes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLRequest SQLRequest = new SQLRequest();
        bool All = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public string[] edit(string text)
        {
            char[] delimiterChars = { ' ', ',', ':', '/', '=', '&' };
            return text.Split(delimiterChars);

        }
        public string editText(string text)
        {

            string g = text;
            g = string.Join(" ", g.Split(' ')
            .Select(x =>
                string.Concat(
                    x.Length >= 1 ? x.Substring(0, 1).ToUpper() : "",
                    x.Length > 1 ? x.Substring(1, x.Length - 1).ToLower() : "")
                )); ;
            return g;

        }

        private void updateDataGrid()
        {
            try
            {

                new Thread(() =>
                {
                    grid.Dispatcher.Invoke(() => grid.ItemsSource = null);

                    if (allData.Dispatcher.Invoke(() => allData.IsChecked) == false)
                    {
                        if (FirmName.Dispatcher.Invoke(() => FirmName.Text) == "")
                        {
                            return;
                        }
                        if (JurAddress.Dispatcher.Invoke(() => JurAddress.Text) == "")
                        {
                            return;
                        }
                        if (PostAddress.Dispatcher.Invoke(() => PostAddress.Text) == "")
                        {
                            return;
                        }
                    }

                    DataSet dataSet = SQLRequest.Request(@$"SELECT FirmDS.Name,
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
                                                                    { (All ? "" : $"AND FirmDS.Name IN('{editText(FirmName.Dispatcher.Invoke(() => FirmName.Text))}') AND")}
                                            { (All ? "" : $" FirmDS.JueCityID IN('{JurAddress.Dispatcher.Invoke(() => JurAddress.Text)}') AND")}
                                            { (All ? "" : $" FirmDS.Post_city_iD IN('{string.Join("','", edit(PostAddress.Dispatcher.Invoke(() => PostAddress.Text)))}PostAddress.Dispatcher.Invoke(() => PostAddress.Text)')")} INNER JOIN                    
                                                                    RegionDS ON CityDS.RegionID = RegionDS.RegionID INNER JOIN
                                                                    UserDS ON FirmDS.UserID = UserDS.UserID INNER JOIN
                                                                    StatusOfLocalsDS ON CityDS.StatusOfLocalsID = StatusOfLocalsDS.StatusOfLocalsID INNER JOIN
                                                                    OwnerShipsDS ON FirmDS.OwnerShipsID = OwnerShipsDS.OwnerShipsID");
                    grid.Dispatcher.Invoke(() => grid.ItemsSource = dataSet.Tables[0].DefaultView);
                }).Start();
            }
            catch (Exception)
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void CityName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (allData.IsChecked == true)
            {
                MessageBox.Show("Ошибка!\nВыключите функцию (Все данные) для ввода данных");
                return;
            }
            new Thread(updateDataGrid).Start();
        }

        private void allData_Checked(object sender, RoutedEventArgs e)
        {
            All = true;
            updateDataGrid();
        }
        private void allData_Unchecked(object sender, RoutedEventArgs e)
        {
            All = false;
        }
    }
}
