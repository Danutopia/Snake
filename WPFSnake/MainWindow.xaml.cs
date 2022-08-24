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
using System.Media;

namespace WPFSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer Timer = new DispatcherTimer();
        private Random Random = new Random();
        private List<Quadrat> Snake = new List<Quadrat>();
        private Letter Letter;
        private Score PunkteStand= new Score();
        private List<Letter> LetterList= new List<Letter>();
        private SoundHandler SoundHandler = new SoundHandler();
        
        readonly int head = 0;        
        private bool lose = false;
        private bool lvlselect = false;
        private bool gamestart = false;
        private bool border = false;
        public string direction = "rechts";

        public MainWindow()
        {
            InitializeComponent();
        }

        void Animation(object sender, EventArgs e)
        {

            Snake[head].MoveTo(direction,Snake);
            lose = Snake[head].BorderCheck(Gamefield, border);

            if (lose)
            {
                Lose();
                return;
            }    
            bool hit = Snake[head].HitCheck(direction, Letter);

            if (hit)
            {
                Hit();
            }
            Reset();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {


            if (lvl1.IsChecked == true)
            {
                border = false;
                lvlselect = true;
            }
            if (lvl2.IsChecked == true)
            {
                border = true;
                lvlselect = true;
            }
            if (lvlselect == false)
                return;

            Timer.Interval = TimeSpan.FromMilliseconds(150);
            Timer.Tick += Animation; //Delegate

            Start.Visibility = Visibility.Hidden;
            gamestart = true;
            labelScore.Content = PunkteStand.label;
            labelWord.Content = "Word ";

            Letter = new Letter(Random.Next(0, (int)Gamefield.Width / 10) * 10, Random.Next(0, (int)Gamefield.Height / 10) * 10);
            Snake.Add(new Quadrat(Gamefield));
            LetterList.Add(Letter);

            Snake[head].Draw(Gamefield);
            Letter.Draw(Gamefield);
            
            Timer.Start();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamestart)
            {
                switch (e.Key)
                {
                    case Key.Up or Key.W:
                        direction = "oben"; break;
                    case Key.Left or Key.A:
                        direction = "links"; break;
                    case Key.Down or Key.S:
                        direction = "unten"; break;
                    case Key.Right or Key.D:
                        direction = "rechts"; break;
                }
            }
        }

        public void AddBody()
        {
            Console.WriteLine("Debug");
            Quadrat newSnakePart = new Quadrat();
            newSnakePart.X= Snake[Snake.Count-1].X;
            newSnakePart.Y= Snake[Snake.Count-1].Y;

            Snake.Add(newSnakePart);
            
            Reset();
        }
        public void Hit()
        {

            SoundHandler.playDefaultSound("collect");
            PunkteStand.score++;
            PunkteStand.label = "Score: " + PunkteStand.score;
            labelScore.Content = PunkteStand.label;

            AddBody();
            LetterList.Clear();
            Letter = new Letter(Random.Next(0, (int)Gamefield.Width / 10) * 10, Random.Next(0, (int)Gamefield.Height / 10) * 10);
            LetterList.Add(Letter);
            Reset();
        }

        private void Reset()
        {
            Gamefield.Children.Clear();
            
            for(int i = 0; i < Snake.Count; i++)
            {
                Snake[i].Draw(Gamefield);
            }
            for (int i = 0; i < LetterList.Count; i++)
            {
                LetterList[i].Draw(Gamefield);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Lose()
        {
            SoundHandler.playDefaultSound("collect");
            MessageBox.Show("Du hast verloren\n Du hattest " + PunkteStand.score + "Punkte");
            PunkteStand.score = 0;
            Gamefield.Children.Clear();
            Start.Visibility = Visibility.Visible;
            LetterList.Clear();
            Snake.Clear();
            Timer.Stop();
        }
    }
}
