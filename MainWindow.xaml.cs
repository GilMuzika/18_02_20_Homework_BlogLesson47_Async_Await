using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _18_02_20_Homework_BlogLesson47_Async_Await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task _runningTimerTask;
        private CancellationTokenSource _cancelToken = new CancellationTokenSource();
        private System.Timers.Timer _timer = new System.Timers.Timer();
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

            Label lblMyLabel = new Label();

            lblShowTimerResult.Content = String.Empty;
            
            

            _timer.Interval = 100;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                _runningTimerTask = Task.Run(() => 
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        lblShowTimerResult.Content = DateTime.Now.Millisecond;
                        if (DateTime.Now.Millisecond % 2 == 0) lblShowTimerResult.Background = new SolidColorBrush(Color.FromArgb(255, 64, 226, 110));
                        else lblShowTimerResult.Background = new SolidColorBrush(Color.FromArgb(255, 32, 107, 213));
                    });

                    try
                    {
                        _cancelToken.Token.ThrowIfCancellationRequested();
                    }
                    catch(OperationCanceledException)
                    {
                        _timer.Stop();
                    }

                });
              
                
            };

            btnTurnOnTimer.Click += (object sender, RoutedEventArgs e) => 
            {

                _cancelToken = new CancellationTokenSource();
                _timer.Start();
            };
            
            

        }

        private void btnTextButton_Click(object sender, RoutedEventArgs e)
        {
            _cancelToken.Cancel();            
        }
    }
}
