using System;
using System.Timers;
using System.Windows.Controls;
using System.Media;
using System.Windows;

namespace simple_timer
{
    public class TimerLogic
    {
        private int _counter;

        //// Unimplemented: to save/load timer values after closing
        //public int counter
        //{
        //    get { return _counter; }
        //    set { _counter = value; }
        //}

        private bool _is_running = false;
        public bool is_running
        {
            get { return _is_running; }
        }

        private ComboBox x, y, z;
        private Label a, b, c;
        private Button p, start, b_notif;
        private TextBlock s;

        private MainWindow mw;

        private System.Windows.Forms.NotifyIcon ni;

        private Timer timer = new Timer(1000);
        private Timer timeout = new Timer(30000);

        private SoundPlayer notif = new SoundPlayer();

        private string sound_path;

        public TimerLogic(ComboBox cx, ComboBox cy, ComboBox cz, Label ca, Label cb, Label cc, Button cp, Button cstart, TextBlock cs, MainWindow cmw, System.Windows.Forms.NotifyIcon cni, string csound_path, Button c_bnotiif)
        {
            x = cx;
            y = cy;
            z = cz;

            a = ca;
            b = cb;
            c = cc;

            p = cp;
            start = cstart;

            s = cs;

            mw = cmw;

            ni = cni;

            sound_path = csound_path;
            b_notif = c_bnotiif;
        }

        private void FillContent()
        {
            a.Content = _counter / 3600;
            b.Content = (_counter % 3600) / 60;
            c.Content = (_counter % 3600) % 60;
        }

        public void Start()
        {
            _counter = int.Parse(x.Text) * 3600 + int.Parse(y.Text) * 60 + int.Parse(z.Text);

            timer.Elapsed -= Timer_Elapsed;
            timer.Stop();
            timer.Elapsed += Timer_Elapsed;
            FillContent();
            timer.Start();

            notif.Dispose();

            timeout.Elapsed -= Timeout_Elapsed;
            timeout.Stop();

            _is_running = true;

            start.Content = "Restart";
            start.ToolTip = "Double-click to restart";

            b_notif.IsEnabled = false;

            s.Text = "";

            if (_counter > 0)
            {
                p.IsEnabled = true;
                p.Content = "Stop";
                p.Visibility = System.Windows.Visibility.Visible;
            }

            notif.Stop();
            //notif.Dispose();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_counter > 0)
            {
                _counter--;
            }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                FillContent();

                if (_counter == 0)
                {
                    p.Content = "Stop";
                    p.Visibility = System.Windows.Visibility.Hidden;
                    p.IsEnabled = false;

                    timeout.Elapsed += Timeout_Elapsed;
                    timeout.Start();

                    timer.Stop();
                    s.Text = "Time's up!";

                    //// New Thread for notification sound, initiate playback
                    //System.Threading.Thread sound = new System.Threading.Thread(() => this.NotificationSound());
                    //sound.Start();
                    NotificationSound();

                    mw.ShowActivated = false;
                    mw.Show();
                    mw.WindowState = WindowState.Normal;

                    //ni.Visible = false;
                }
            });
        }

        private void Timeout_Elapsed(object sender, ElapsedEventArgs e)
        {
            timeout.Elapsed -= Timeout_Elapsed;

            notif.Stop();
            notif.Dispose();

            timeout.Stop();
        }

        private void NotificationSound()
        {
            if (sound_path.Length != 0 || sound_path != null)
            {
                notif.SoundLocation = @sound_path;
                try
                {
                    notif.Load();
                }
                catch (Exception ex1)
                {
                    if (ex1 is System.IO.FileNotFoundException || ex1 is System.NullReferenceException)
                    {
                        notif.Dispose();
                        SystemSounds.Exclamation.Play();
                    }
                }
                if (notif.IsLoadCompleted)
                {
                    try
                    {
                        notif.Play();
                    }
                    catch (Exception ex2)
                    {
                        if (ex2 is System.InvalidOperationException || ex2 is System.IO.FileNotFoundException)
                        {
                            notif.Dispose();
                            SystemSounds.Exclamation.Play();
                        }
                    }
                }
                else
                {
                    notif.Dispose();
                    SystemSounds.Exclamation.Play();
                }
            }
            else
            {
                notif.Dispose();
            }
        }

        public void Pause()
        {
            if (timer.Enabled)
            {
                timer.Stop();
                p.Content = "Resume";
            }
            else
            {
                timer.Start();
                p.Content = "Stop";
            }
        }

        public void Reset()
        {
            if (_is_running)
            {
                timer.Elapsed -= Timer_Elapsed;
                timer.Stop();

                timeout.Elapsed -= Timeout_Elapsed;
                timeout.Stop();

                _is_running = false;

                start.Content = "Start";
                start.ToolTip = null;

                b_notif.IsEnabled = true;

                a.Content = 0;
                b.Content = 0;
                c.Content = 0;

                s.Text = "";

                //// Resetting ComboBoxes (where you set the actual timer)
                //x.SelectedIndex = 0;
                //y.SelectedIndex = 0;
                //z.SelectedIndex = 0;

                p.Visibility = System.Windows.Visibility.Hidden;
                p.IsEnabled = false;

                notif.Stop();
                notif.Dispose();
            }
        }
    }
}