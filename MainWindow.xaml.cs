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

            InitializeSnake();

            InitializeBoard();

            DrawEmptyBoard();

            test();
        }

        int i = 5;

        private async void test()
        {
            await Task.Delay(3000);
            board[i,i].Source = new BitmapImage(new Uri(@"./images/Fruit.png", UriKind.Relative));
            i--;
            if (i == 0)
                return;
            test();
        }

        private void InitializeSnake()
        {
            snake = new Snake();
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
    }
}