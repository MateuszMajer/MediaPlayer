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
using System.Windows.Threading;

namespace PlayerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            VideoWindow.Play();
            if (timer != null)
                timer.Start();
        }

        private void PauseButton(object sender, RoutedEventArgs e)
        {
            VideoWindow.Pause();
            if (timer != null)
                timer.Stop();
        }

        private void StopButton(object sender, RoutedEventArgs e)
        {
            VideoWindow.Stop();
            if (timer != null)
                timer.Stop();
        }

        private void MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VideoWindow.ScrubbingEnabled = true;
            VideoWindow.Stop();
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            TimerSlider.Maximum = VideoWindow.NaturalDuration.TimeSpan.TotalSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            FullTimeOfVideo.Content = VideoWindow.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            VideoWindow.Play();
            VideoWindow.Pause();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerSlider.Value = VideoWindow.Position.TotalSeconds;
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoWindow.Position = TimeSpan.FromSeconds(TimerSlider.Value);
        }

        private void DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            VideoWindow.Pause();
            if(timer!=null)
            timer.Stop();
        }

        private void DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            VideoWindow.Play();
            timer.Start();
        }
    }
}
