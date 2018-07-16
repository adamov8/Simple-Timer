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
using System.Timers;
using System.Windows.Threading;

namespace simple_timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public int iv;

        public static Timer t1 = new Timer(1000);
        private void timer_start_1_Click(object sender, RoutedEventArgs e)
        {
            t1.Elapsed -= T1_Elapsed;
            t1.Stop();
            t1.Elapsed += T1_Elapsed;
            timer_label_1.Content = iv;
            t1.Start();
        }

        private void T1_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (iv > 0)
            {
                iv--;
            }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                timer_label_1.Content = iv;
            });
        }

        private void tb1_LostFocus(object sender, RoutedEventArgs e)
        {
            int number;
            bool convert = int.TryParse(tb1_m.Text, out number);
            if (convert == true)
            {
                iv = /*int.Parse(tb1_h.Text) * 3600 +*/ int.Parse(tb1_m.Text) * 60 /*+ int.Parse(tb1_s.Text)*/;
            }
            else
            {
                tb1_m.Text = "";
                MessageBox.Show("Please only use numbers here!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void timer_reset_1_Click(object sender, RoutedEventArgs e)
        {
            t1.Elapsed -= T1_Elapsed;
            t1.Stop();
            timer_label_1.Content = 0;
            tb1_m.Text = "";
        }
    }
}