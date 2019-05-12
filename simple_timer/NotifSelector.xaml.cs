using System;
using System.Windows;
using System.Runtime.InteropServices;

namespace simple_timer
{
    /// <summary>
    /// Interaction logic for NotifSelector.xaml
    /// </summary>
    public partial class NotifSelector : Window
    {
        public NotifSelector()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        // Hide close (x) button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private string _filepath;
        public string filepath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        private void b_notifConfirm_Click(object sender, RoutedEventArgs e)
        {
            _filepath = tb_notifPath.Text;
            this.Hide();
        }
        private void b_notifCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        // Save/Load last window position
        private void Window_Initialized(object sender, EventArgs e)
        {
            this.Left = MySettings.Default.NotifWindowPositionLeft;
            this.Top = MySettings.Default.NotifWindowPositionTop;
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MySettings.Default.NotifWindowPositionTop = this.Top;
            MySettings.Default.NotifWindowPositionLeft = this.Left;

            MySettings.Default.Save();
        }
    }
}
