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
                    prog.Dispatcher.Invoke(() =>
                    {
                        prog.Value = i;

                    });
                    Thread.Sleep(15);
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
