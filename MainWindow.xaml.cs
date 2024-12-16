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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Image[,] board;

        private Snake snake;

        public MainWindow()
        {
            InitializeComponent();

            InitializeBoard();

            DrawEmptyBoard();

            InitializeSnake();

            snake.fieldStateChanged += UpdateBoard;

            snake.GenerateNewFruit();

            callNextMove();
        }
        private async void callNextMove()
        {
            await Task.Delay(100);
            snake.MakeNextMove();
            callNextMove();
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
                    imageField.Width = 50;
                    GameGrid.Children.Add(imageField);
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
                    board[i, j].Source = new BitmapImage(new Uri(@"./images/emptyField.jpg", UriKind.Relative));
                }
            }
        }

        public void UpdateBoard(Tuple<int, int> field, FieldState fieldState, Direction? direction)
        {
            switch (fieldState)
            {
                case FieldState.Empty:
                    board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/emptyField.jpg", UriKind.Relative));
                    break;
                case FieldState.Snake:
                    board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakePart.png", UriKind.Relative));
                    break;
                case FieldState.Fruit:
                    board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/fruit.png", UriKind.Relative));
                    break;
                case FieldState.SnakeHead:
                    //todo add rotating image based on direction
                    board[field.Item1, field.Item2].Source = new BitmapImage(new Uri(@"./images/snakeHead.png", UriKind.Relative));
                    break;
            }
            
        }

        private void ChangeDirectionHandler(object sender, KeyEventArgs e)
        {
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
    }
}