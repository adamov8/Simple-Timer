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
            int number;
            bool convert_h = int.TryParse(comboBox_h.Text, out number);
            bool convert_m = int.TryParse(comboBox_m.Text, out number);
            bool convert_s = int.TryParse(comboBox_s.Text, out number);
            if (convert_m && convert_m && convert_s)
            {
                iv = int.Parse(comboBox_h.Text) * 3600 + int.Parse(comboBox_m.Text) * 60 + int.Parse(comboBox_s.Text);
            }

            t1.Elapsed -= T1_Elapsed;
            t1.Stop();
            t1.Elapsed += T1_Elapsed;
            timer_label_h.Content = iv / 3600;
            timer_label_m.Content = (iv % 3600) / 60;
            timer_label_s.Content = (iv % 3600) % 60;
            t1.Start();

            if(iv > 0)
            {
                timer_pause_1.IsEnabled = true;
            }
        }

        private void T1_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (iv > 0)
            {
                iv--;
            }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                timer_label_h.Content = iv / 3600;
                timer_label_m.Content = (iv % 3600) / 60;
                timer_label_s.Content = (iv % 3600) % 60;
            });
        }

        private void timer_pause_1_Click(object sender, RoutedEventArgs e)
        {
            if (t1.Enabled)
            {
                t1.Stop();
                timer_pause_1.Content = "Resume";
            }
            else
            {
                t1.Start();
                timer_pause_1.Content = "Stop";
            }
        }
        private void timer_pause_1_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_1.IsEnabled = false;
        }

        private void timer_reset_1_Click(object sender, RoutedEventArgs e)
        {
            t1.Elapsed -= T1_Elapsed;
            t1.Stop();
            timer_label_h.Content = 0;
            timer_label_m.Content = 0;
            timer_label_s.Content = 0;

            comboBox_h.SelectedIndex = 0;
            comboBox_m.SelectedIndex = 0;
            comboBox_s.SelectedIndex = 0;

            timer_pause_1.IsEnabled = false;
        }

        private void comboBox_h_Loaded(object sender, RoutedEventArgs e)
        {
            int[] hours = new int[24];
            for (int h = 0; h < hours.Length; h++)
            {
                hours[h] = h;
            }
            comboBox_h.ItemsSource = hours;
            comboBox_h.SelectedIndex = 0;
        }

        private void comboBox_m_Loaded(object sender, RoutedEventArgs e)
        {
            int[] minutes = new int[61];
            for (int m = 0; m < minutes.Length; m++)
            {
                minutes[m] = m;
            }
            comboBox_m.ItemsSource = minutes;
            comboBox_m.SelectedIndex = 0;
        }

        private void comboBox_s_Loaded(object sender, RoutedEventArgs e)
        {
            int[] seconds = new int[61];
            for (int s = 0; s < seconds.Length; s++)
            {
                seconds[s] = s;
            }
            comboBox_s.ItemsSource = seconds;
            comboBox_s.SelectedIndex = 0;
        }
    }
}