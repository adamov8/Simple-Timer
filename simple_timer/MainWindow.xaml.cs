using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace simple_timer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // prcocess manager to only let one instance run at a time
            Process proc = Process.GetCurrentProcess();
            int count = Process.GetProcesses().Where(p =>
                             p.ProcessName == proc.ProcessName).Count();
            if (count > 1)
            {
                MessageBox.Show("Simple Timer is already running.", "Duplicate process initialization attempt", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                App.Current.Shutdown();
            }


            InitializeComponent();

            ResizeMode = ResizeMode.CanMinimize;

            NotIcon();
        }

        // disable HW acceleration
        protected override void OnSourceInitialized(EventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
                hwndSource.CompositionTarget.RenderMode = RenderMode.SoftwareOnly;

            base.OnSourceInitialized(e);
        }

        private System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();

        // Notification area icon
        private void NotIcon()
        {
            ni.Icon = new System.Drawing.Icon(global::simple_timer.Properties.Resources.Timer_16x, 16, 16);
            ni.Text = "Simple Timer";
            ni.Visible = true;
            ni.Click +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate();
                    //ni.Visible = false;
                };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            ni.Visible = true;
            base.OnStateChanged(e);
        }

        // Timers
        TimerLogic tm;
        TimerLogic tm2;
        TimerLogic tm3;

        // Notification sound selector windows
        NotifSelector notif1 = new NotifSelector();
        NotifSelector notif2 = new NotifSelector();
        NotifSelector notif3 = new NotifSelector();

        // Timer 1
        private void timer_start_1_Click(object sender, RoutedEventArgs e)
        {
            if (tm == null)
            {
                tm = new TimerLogic(comboBox_h, comboBox_m, comboBox_s, timer_label_h, timer_label_m, timer_label_s, timer_pause_1, timer_start_1, textBlock, this, ni, notif1.filepath, b_notif_1);
            }

            if (!tm.is_running)
            {
                tm.Start();
            }
        }
        private void timer_start_1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tm.is_running)
            {
                tm.Start();
            }
        }
        private void timer_pause_1_Click(object sender, RoutedEventArgs e)
        {
            tm.Pause();
        }
        private void timer_pause_1_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_1.IsEnabled = false;
            timer_pause_1.Visibility = Visibility.Hidden;
        }
        private void timer_reset_1_Click(object sender, RoutedEventArgs e)
        {
            if (tm != null)
            {
                tm.Reset();
                tm = null;
            }
        }

        // Timer 2
        private void timer_start_2_Click(object sender, RoutedEventArgs e)
        {
            if (tm2 == null)
            {
                tm2 = new TimerLogic(comboBox_h2, comboBox_m2, comboBox_s2, timer_label_h2, timer_label_m2, timer_label_s2, timer_pause_2, timer_start_2, textBlock_2, this, ni, notif2.filepath, b_notif_2);
            }
            if (!tm2.is_running)
            {
                tm2.Start();
            }
        }
        private void timer_start_2_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tm2.is_running)
            {
                tm2.Start();
            }
        }
        private void timer_pause_2_Click(object sender, RoutedEventArgs e)
        {
            tm2.Pause();
        }
        private void timer_pause_2_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_2.IsEnabled = false;
            timer_pause_2.Visibility = Visibility.Hidden;
        }
        private void timer_reset_2_Click(object sender, RoutedEventArgs e)
        {
            if (tm2 != null)
            {
                tm2.Reset();
                tm2 = null;
            }
        }

        // Timer 3
        private void timer_start_3_Click(object sender, RoutedEventArgs e)
        {
            if (tm3 == null)
            {
                tm3 = new TimerLogic(comboBox_h3, comboBox_m3, comboBox_s3, timer_label_h3, timer_label_m3, timer_label_s3, timer_pause_3, timer_start_3, textBlock_3, this, ni, notif3.filepath, b_notif_3);
            }
            if (!tm3.is_running)
            {
                tm3.Start();
            }
        }
        private void timer_start_3_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tm3.is_running)
            {
                tm3.Start();
            }
        }
        private void timer_pause_3_Click(object sender, RoutedEventArgs e)
        {
            tm3.Pause();
        }
        private void timer_pause_3_Loaded(object sender, RoutedEventArgs e)
        {
            timer_pause_3.IsEnabled = false;
            timer_pause_3.Visibility = Visibility.Hidden;
        }
        private void timer_reset_3_Click(object sender, RoutedEventArgs e)
        {
            if (tm3 != null)
            {
                tm3.Reset();
                tm3 = null;
            }
        }

        // Saving values
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MySettings.Default.cb_h = comboBox_h.Text;
            MySettings.Default.cb_m = comboBox_m.Text;
            MySettings.Default.cb_s = comboBox_s.Text;

            MySettings.Default.cb_h2 = comboBox_h2.Text;
            MySettings.Default.cb_m2 = comboBox_m2.Text;
            MySettings.Default.cb_s2 = comboBox_s2.Text;

            MySettings.Default.cb_h3 = comboBox_h3.Text;
            MySettings.Default.cb_m3 = comboBox_m3.Text;
            MySettings.Default.cb_s3 = comboBox_s3.Text;

            MySettings.Default.tb = tb.Text;
            MySettings.Default.tb2 = tb2.Text;
            MySettings.Default.tb3 = tb3.Text;

            MySettings.Default.filepath_1 = notif1.filepath;
            MySettings.Default.filepath_2 = notif2.filepath;
            MySettings.Default.filepath_3 = notif3.filepath;

            // Save last window position
            MySettings.Default.WindowPositionTop = this.Top;
            MySettings.Default.WindowPositionLeft = this.Left;

            MySettings.Default.Save();

            notif1.Close();
            notif2.Close();
            notif3.Close();

            ni.Visible = false;
            ni = null;
        }

        // Load saved values
        private void tb1_Loaded(object sender, RoutedEventArgs e)
        {
            tb.Text = MySettings.Default.tb;
        }

        private void tb2_Loaded(object sender, RoutedEventArgs e)
        {
            tb2.Text = MySettings.Default.tb2;
        }

        private void tb3_Loaded(object sender, RoutedEventArgs e)
        {
            tb3.Text = MySettings.Default.tb3;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            // Load last window postion
            this.Left = MySettings.Default.WindowPositionLeft;
            this.Top = MySettings.Default.WindowPositionTop;

            // Load NotifSelector sound
            notif1.filepath = MySettings.Default.filepath_1;
            notif1.tb_notifPath.Text = MySettings.Default.filepath_1;

            notif2.filepath = MySettings.Default.filepath_2;
            notif2.tb_notifPath.Text = MySettings.Default.filepath_2;

            notif3.filepath = MySettings.Default.filepath_3;
            notif3.tb_notifPath.Text = MySettings.Default.filepath_3;
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

            comboBox_h2.ItemsSource = hours;
            comboBox_h2.SelectedIndex = 0;

            comboBox_h3.ItemsSource = hours;
            comboBox_h3.SelectedIndex = 0;

            // Load saved values
            comboBox_h.Text = MySettings.Default.cb_h;
            comboBox_h2.Text = MySettings.Default.cb_h2;
            comboBox_h3.Text = MySettings.Default.cb_h3;
        }

        private void comboBox_m_Loaded(object sender, RoutedEventArgs e)
        {
            int[] minutes_seconds = new int[60];
            for (int ms = 0; ms < minutes_seconds.Length; ms++)
            {
                minutes_seconds[ms] = ms;
            }
            comboBox_m.ItemsSource = minutes_seconds;
            comboBox_m.SelectedIndex = 0;
            comboBox_s.ItemsSource = minutes_seconds;
            comboBox_s.SelectedIndex = 0;

            comboBox_m2.ItemsSource = minutes_seconds;
            comboBox_m2.SelectedIndex = 0;
            comboBox_s2.ItemsSource = minutes_seconds;
            comboBox_s2.SelectedIndex = 0;

            comboBox_m3.ItemsSource = minutes_seconds;
            comboBox_m3.SelectedIndex = 0;
            comboBox_s3.ItemsSource = minutes_seconds;
            comboBox_s3.SelectedIndex = 0;

            // Load saved values
            comboBox_m.Text = MySettings.Default.cb_m;
            comboBox_s.Text = MySettings.Default.cb_s;

            comboBox_m2.Text = MySettings.Default.cb_m2;
            comboBox_s2.Text = MySettings.Default.cb_s2;

            comboBox_m3.Text = MySettings.Default.cb_m3;
            comboBox_s3.Text = MySettings.Default.cb_s3;
        }

        // Timer 1 - Notification sound selector button
        private void b_notif_1_Click(object sender, RoutedEventArgs e)
        {
            notif1.Title = "Timer 1 - Select notification sound";
            notif1.Owner = this;
            notif1.ShowDialog();

            notif1.tb_notifPath.Text = notif1.filepath;
        }

        // Timer 2 - Notification sound selector button
        private void b_notif_2_Click(object sender, RoutedEventArgs e)
        {
            notif2.Title = "Timer 2 - Select notification sound";
            notif2.Owner = this;
            notif2.ShowDialog();

            notif2.tb_notifPath.Text = notif2.filepath;
        }

        // Timer 3 - Notification sound selector button
        private void b_notif_3_Click(object sender, RoutedEventArgs e)
        {
            notif3.Title = "Timer 3 - Select notification sound";
            notif3.Owner = this;
            notif3.ShowDialog();

            notif3.tb_notifPath.Text = notif3.filepath;
        }
    }
}