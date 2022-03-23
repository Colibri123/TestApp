using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
            anima();
            
        }

        public void anima()
        {
            
            new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    if (i == 10)
                    {
                        load.Dispatcher.Invoke(()=> load.Content = "загрузка.") ;
                    }
                    if (i == 20)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка..");
                    }
                    if (i == 30)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка...");
                    }
                    if (i == 40)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка....");
                    }
                    if (i == 50)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка.....");
                    }
                    if (i == 60)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка.");
                    }
                    if (i == 70)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка..");
                    }
                    if (i == 80)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка...");
                    }
                    if (i == 90)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка....");

                    }
                    if (i == 100)
                    {
                        load.Dispatcher.Invoke(() => load.Content = "загрузка.....");
                    }
                    prog.Dispatcher.Invoke(() =>
                    {
                        prog.Value = i;

                    });
                    Thread.Sleep(10);
                };
                Dispatcher.Invoke(() => 
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                });
                
            }).Start();
        
                
            
        }
    }
}
