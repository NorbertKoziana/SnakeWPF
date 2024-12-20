﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Image[,] board;

        private Snake snake;

        private bool _isGameActive;

        public MainWindow()
        {
            InitializeComponent();

            InitializeBoard();

            StartGame();
        }

        private void StartGame()
        {
            DrawEmptyBoard();

            InitializeSnake();

            BindScore();

            snake.fieldStateChanged += UpdateBoard;
            snake.gameEnded += StopGame;

            snake.GenerateNewFruit();

            InfoText.Text = "Score: ";

            startMovingSnake();
        }

        private async void startMovingSnake()
        {
            _isGameActive = true;
            while (_isGameActive)
            {
                await Task.Delay(150);
                snake.MakeNextMove();
            }
        }

        private void InitializeSnake()
        {
            snake = new Snake(GameGrid.Rows, GameGrid.Columns);
        }

        private void InitializeBoard()
        {
            board = new Image[GameGrid.Rows, GameGrid.Columns];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Image imageField = new Image();

                    Border imageBorder = new Border();
                    imageBorder.BorderThickness = new Thickness(1);
                    imageBorder.BorderBrush = new SolidColorBrush(Colors.DimGray) { Opacity = 0.3 };
                    imageBorder.Child = imageField;

                    GameGrid.Children.Add(imageBorder);
                    board[i, j] = imageField;
                }
            }
        }

        public void DrawEmptyBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j].Visibility = Visibility.Hidden;
                }
            }
        }

        public void UpdateBoard(Tuple<int, int> field, FieldState fieldState, Direction? direction)
        {
            switch (fieldState)
            {
                case FieldState.Empty:
                    board[field.Item1, field.Item2].Visibility = Visibility.Hidden;
                    break;
                case FieldState.Snake:
                    board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakePart.png", UriKind.Relative));
                    board[field.Item1, field.Item2].Visibility = Visibility.Visible;
                    break;
                case FieldState.Fruit:
                    board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/fruit.png", UriKind.Relative));
                    board[field.Item1, field.Item2].Visibility = Visibility.Visible;
                    break;
                case FieldState.SnakeHead:
                    switch (direction)
                    {
                        case Direction.Left:
                            board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakeHeadLeft.png", UriKind.Relative));
                            break;
                        case Direction.Right:
                            board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakeHeadRight.png", UriKind.Relative));
                            break;
                        case Direction.Top:
                            board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakeHeadTop.png", UriKind.Relative));
                            break;
                        case Direction.Bottom:
                            board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakeHeadBottom.png", UriKind.Relative));
                            break;
                    }
                    board[field.Item1, field.Item2].Visibility = Visibility.Visible;
                    break;
            }
            
        }

        private void KeyPressEventHandler(object sender, KeyEventArgs e)
        {
            if (!_isGameActive)
            {
                if(e.Key == Key.Space)
                {
                    StartGame();
                }

                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    snake.ChangeDirection(Direction.Top);
                    break;
                case Key.Down:
                    snake.ChangeDirection(Direction.Bottom);
                    break;
                case Key.Left:
                    snake.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    snake.ChangeDirection(Direction.Right);
                    break;
            }
        }
        private void BindScore()
        {
            var scoreBinding = new Binding("Score") { Source = snake };
            ScoreText.SetBinding(TextBlock.TextProperty, scoreBinding);
        }

        private void StopGame()
        {
            InfoText.Text = "You lost with score: " + snake.Score;
            ScoreText.Text = "Press space to continue";
            _isGameActive = false;
        }

    }
}