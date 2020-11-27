using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CwWpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timeLeft = 0;
        private static int size = 24;
        private int[] numbers = new int[size];
        private int score;
        private bool isGameStart = false;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            var uri = new Uri("https://st.depositphotos.com/2061995/2701/v/600/depositphotos_27016067-stock-illustration-all-seeing-eye-vector.jpg");
            var bitmap = new BitmapImage(uri);
            Img.Source = bitmap;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += dtTicker;
            timer.Start();
            GameStart();
        }

        private void dtTicker(object sender, EventArgs e)
        {
            timeLeft--;
            time.Content = $"Time: {timeLeft.ToString()} sec.";
            if (timeLeft <= 0)
                isGameStop();
        }
        private void GameStart()
        {
            resetAll();
            foreach (Button item in panel.Children.OfType<Button>())
            {
                item.Background = Brushes.White;
            }
            complication();
            fillArr();
        }
        private void complication()
        {
            if (sliderLvl.Value == 1) { timeLeft = 90; }
            if (sliderLvl.Value == 50) { timeLeft = 60; }
            if (sliderLvl.Value == 100) { timeLeft = 30; }
        }

        private void fillArr()
        {
            int b = 1;
            for (int i = 0; i < size; i++)
            {
                numbers[i] = b++;
            }
            randArr();
            int j = 0;
            foreach (Button item in panel.Children.OfType<Button>())
            {
                (item.Content) = numbers[j++].ToString();
            }
        }
        private void randArr()
        {
            Random rand = new Random();

            for (int i = numbers.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                int tmp = numbers[j];
                numbers[j] = numbers[i];
                numbers[i] = tmp;
            }
        }
        private void isGameStop()
        {
            timer.Stop();
            MessageBox.Show($"Your score: {score-1}");
            isGameStart = false;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (isGameStart == true)
            {
                string number;
                Button button = (Button)sender;
                number = button.Content.ToString();
                if (number == score.ToString())
                {
                    score++;
                    button.Background = Brushes.Green;
                    bar.Value += 4.3;
                }
                else if (number != score.ToString())
                {
                    button.Background = Brushes.Red;
                }
                if (bar.Value >= 100)
                {
                    isGameStop();
                }
            }
        }

        private void resetAll()
        {
            isGameStart = true;
            score = 1;
            bar.Value = 0;
        }
    }
}
