using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace simple_timer
{
    public class TimerLogic
    {
        public int iv;
        public Timer timer = new Timer(1000);

        public void Start(ComboBox x, ComboBox y, ComboBox z, Label a, Label b, Label c, Button p, ElapsedEventHandler e)
        {

            iv = int.Parse(x.Text) * 3600 + int.Parse(y.Text) * 60 + int.Parse(z.Text);

            timer.Elapsed -= e;
            timer.Stop();
            timer.Elapsed += e;
            a.Content = iv / 3600;
            b.Content = (iv % 3600) / 60;
            c.Content = (iv % 3600) % 60;
            timer.Start();

            if (iv > 0)
            {
                p.IsEnabled = true;
                p.Content = "Stop";
            }
        }
        public void Tick(Label a, Label b, Label c)
        {
            if (iv > 0)
            {
                iv--;
            }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                a.Content = iv / 3600;
                b.Content = (iv % 3600) / 60;
                c.Content = (iv % 3600) % 60;
            });
        }
        public void Pause(Button p)
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
        public void Reset(ComboBox x, ComboBox y, ComboBox z, Label a, Label b, Label c, Button p, ElapsedEventHandler e)
        {
            timer.Elapsed -= e;
            timer.Stop();
            a.Content = 0;
            b.Content = 0;
            c.Content = 0;

            x.SelectedIndex = 0;
            y.SelectedIndex = 0;
            z.SelectedIndex = 0;

            p.IsEnabled = false;
        }
        public void ComboHFill(ComboBox x)
        {
            int[] hours = new int[24];
            for (int h = 0; h < hours.Length; h++)
            {
                hours[h] = h;
            }
            x.ItemsSource = hours;
            x.SelectedIndex = 0;
        }
        public void ComboMSFill(ComboBox yz)
        {
            int[] minutes_seconds = new int[60];
            for (int ms = 0; ms < minutes_seconds.Length; ms++)
            {
                minutes_seconds[ms] = ms;
            }
            yz.ItemsSource = minutes_seconds;
            yz.SelectedIndex = 0;
        }
    }
}
