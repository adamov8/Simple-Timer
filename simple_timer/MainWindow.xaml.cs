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

        TimerLogic tm1 = new TimerLogic();
        TimerLogic tm2 = new TimerLogic();
        TimerLogic tm3 = new TimerLogic();

        // Timer 1
        private void timer_start_1_Click(object sender, RoutedEventArgs e)
        {
            tm1.Start(comboBox_h, comboBox_m,comboBox_s,timer_label_h,timer_label_m,timer_label_s,timer_pause_1,T1_Elapsed);
        }

        private void T1_Elapsed(object sender, ElapsedEventArgs e)
        {
            tm1.Tick(timer_label_h, timer_label_m, timer_label_s);
        }

        private void timer_pause_1_Click(object sender, RoutedEventArgs e)
        {
            tm1.Pause(timer_pause_1);
        }
        private void timer_pause_1_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_1.IsEnabled = false;
        }

        private void timer_reset_1_Click(object sender, RoutedEventArgs e)
        {
            tm1.Reset(comboBox_h, comboBox_m, comboBox_s, timer_label_h, timer_label_m, timer_label_s, timer_pause_1, T1_Elapsed);
        }

        private void comboBox_h_Loaded(object sender, RoutedEventArgs e)
        {
            tm1.ComboHFill(comboBox_h);
        }

        private void comboBox_m_Loaded(object sender, RoutedEventArgs e)
        {
            tm1.ComboMSFill(comboBox_m);
        }

        private void comboBox_s_Loaded(object sender, RoutedEventArgs e)
        {
            tm1.ComboMSFill(comboBox_s);
        }

        // Timer 2
        private void timer_start_2_Click(object sender, RoutedEventArgs e)
        {
            tm2.Start(comboBox_h2, comboBox_m2, comboBox_s2, timer_label_h2, timer_label_m2, timer_label_s2, timer_pause_2, T2_Elapsed);
        }

        private void T2_Elapsed(object sender, ElapsedEventArgs e)
        {
            tm2.Tick(timer_label_h2, timer_label_m2, timer_label_s2);
        }

        private void timer_pause_2_Click(object sender, RoutedEventArgs e)
        {
            tm2.Pause(timer_pause_2);
        }
        private void timer_pause_2_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_2.IsEnabled = false;
        }

        private void timer_reset_2_Click(object sender, RoutedEventArgs e)
        {
            tm2.Reset(comboBox_h2, comboBox_m2, comboBox_s2, timer_label_h2, timer_label_m2, timer_label_s2, timer_pause_2, T2_Elapsed);
        }

        private void comboBox_h2_Loaded(object sender, RoutedEventArgs e)
        {
            tm2.ComboHFill(comboBox_h2);
        }

        private void comboBox_m2_Loaded(object sender, RoutedEventArgs e)
        {
            tm2.ComboMSFill(comboBox_m2);
        }

        private void comboBox_s2_Loaded(object sender, RoutedEventArgs e)
        {
            tm2.ComboMSFill(comboBox_s2);
        }

        // Timer 3
        private void timer_start_3_Click(object sender, RoutedEventArgs e)
        {
            tm3.Start(comboBox_h3, comboBox_m3, comboBox_s3, timer_label_h3, timer_label_m3, timer_label_s3, timer_pause_3, T3_Elapsed);
        }

        private void T3_Elapsed(object sender, ElapsedEventArgs e)
        {
            tm3.Tick(timer_label_h3, timer_label_m3, timer_label_s3);
        }

        private void timer_pause_3_Click(object sender, RoutedEventArgs e)
        {
            tm3.Pause(timer_pause_3);
        }
        private void timer_pause_3_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_3.IsEnabled = false;
        }

        private void timer_reset_3_Click(object sender, RoutedEventArgs e)
        {
            tm3.Reset(comboBox_h3, comboBox_m3, comboBox_s3, timer_label_h3, timer_label_m3, timer_label_s3, timer_pause_3, T3_Elapsed);
        }

        private void comboBox_h3_Loaded(object sender, RoutedEventArgs e)
        {
            tm3.ComboHFill(comboBox_h3);
        }

        private void comboBox_m3_Loaded(object sender, RoutedEventArgs e)
        {
            tm3.ComboMSFill(comboBox_m3);
        }

        private void comboBox_s3_Loaded(object sender, RoutedEventArgs e)
        {
            tm3.ComboMSFill(comboBox_s3);
        }
    }
}