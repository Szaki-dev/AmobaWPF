using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmobaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlayerXScore = 0;
            PlayerOScore = 0;
            CurrentPlayer = "X";
            GenerateGameBoard();
        }

        private Button[,] Buttons;
        private string CurrentPlayer;
        private int PlayerXScore;
        private int PlayerOScore;

        private void GenerateGameBoard()
        {
            GameBoard.Children.Clear();
            Buttons = new Button[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Buttons[i, j] = new Button();
                    Buttons[i, j].Content = "";
                    Buttons[i, j].FontSize = 36;
                    Buttons[i, j].Height = 100;
                    Buttons[i, j].Width = 100;   
                    Buttons[i, j].Click += OnButtonClick;
                    GameBoard.Children.Add(Buttons[i, j]);
                    Grid.SetRow(Buttons[i, j], i);
                    Grid.SetColumn(Buttons[i, j], j);
                }
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = CurrentPlayer;
            button.Foreground = CurrentPlayer == "X" ? Brushes.Red : Brushes.Blue;
            if (CheckWin())
            {
                MessageBox.Show(CurrentPlayer + " nyert!");
                UpdateScore();
                RestartGame();
            }
            else if (IsBoardFull())
            {
                MessageBox.Show("Döntetlen!");
                RestartGame();
            }
            else
            {
                CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
                CurrentText.Text = "Játékos: " + CurrentPlayer;
                CurrentText.Foreground = CurrentPlayer == "X" ? Brushes.Red : Brushes.Blue;
            }
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Buttons[i, 0].Content is string b1 &&
                    Buttons[i, 1].Content is string b2 &&
                    Buttons[i, 2].Content is string b3 &&
                    !string.IsNullOrEmpty(b1) && b1 == b2 && b2 == b3)
                {
                    return true;
                }
                if (Buttons[0, i].Content is string c1 &&
                    Buttons[1, i].Content is string c2 &&
                    Buttons[2, i].Content is string c3 &&
                    !string.IsNullOrEmpty(c1) && c1 == c2 && c2 == c3)
                {
                    return true;
                }
            }
            if (Buttons[0, 0].Content is string d1 &&
                Buttons[1, 1].Content is string d2 &&
                Buttons[2, 2].Content is string d3 &&
                !string.IsNullOrEmpty(d1) && d1 == d2 && d2 == d3)
            {
                return true;
            }

            if (Buttons[0, 2].Content is string d4 &&
                Buttons[1, 1].Content is string d5 &&
                Buttons[2, 0].Content is string d6 &&
                !string.IsNullOrEmpty(d4) && d4 == d5 && d5 == d6)
            {
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (Button button in Buttons)
            {
                if (button.Content.ToString() == "")
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateScore()
        {
            if (CurrentPlayer == "X")
            {
                PlayerXScore++;
            }
            else
            {
                PlayerOScore++;
            }
            Player1ScoreText.Text = $"X: {PlayerXScore.ToString()}";
            Player2ScoreText.Text = $"O: {PlayerOScore.ToString()}";
        }

        private void RestartGame()
        {
            CurrentPlayer = "X";
            CurrentText.Text = "Játékos: X";
            GenerateGameBoard();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }
    }
}