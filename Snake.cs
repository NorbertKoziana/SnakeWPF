using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp3
{
    public class Snake : INotifyPropertyChanged
    {
        public event Action<Tuple<int, int>, FieldState, Direction?> fieldStateChanged;
        public event Action gameEnded;

        public LinkedList<Tuple<int, int>> snakeLocation;
        public Direction snakeHeadDirection;
        public Tuple<int, int> fruitLocation;

        private int _score;
        public int Score {
            get
            {
                return _score;
            }
            set {
                _score = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int boardRows;
        private int boardCols;

        public Snake(int boardRows, int boardCols)
        {
            this.snakeLocation = new LinkedList<Tuple<int, int>>();
            snakeLocation.AddFirst(new Tuple<int, int>(3, 0)); //starting position
            this.snakeHeadDirection = Direction.Right;
            this.boardRows = boardRows;
            this.boardCols = boardCols;
            Score = 0;
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (
                (snakeHeadDirection == Direction.Left && newDirection == Direction.Right) ||
                (snakeHeadDirection == Direction.Right && newDirection == Direction.Left) ||
                (snakeHeadDirection == Direction.Top && newDirection == Direction.Bottom) ||
                (snakeHeadDirection == Direction.Bottom && newDirection == Direction.Top)
            )
                return;

            this.snakeHeadDirection = newDirection;
            fieldStateChanged.Invoke(snakeLocation.First(), FieldState.SnakeHead, newDirection);
        }

        public void MakeNextMove()
        {
            Tuple<int, int> headLocation = snakeLocation.First();

            Tuple<int, int> newHeadLocation = null;

            switch (snakeHeadDirection)
            {
                case Direction.Left:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1, headLocation.Item2 - 1);
                    break;
                case Direction.Right:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1, headLocation.Item2 + 1);
                    break;
                case Direction.Top:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1 - 1, headLocation.Item2);
                    break;
                default:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1 + 1, headLocation.Item2);
                    break;
            }

            if (FieldIsLosing(newHeadLocation))
            {
                gameEnded?.Invoke();
                return;
            }

            //move snake head
            if(snakeLocation.Count > 1)
            {
                fieldStateChanged.Invoke(headLocation, FieldState.Snake, null);
            }
            else
            {
                fieldStateChanged.Invoke(headLocation, FieldState.Empty, null);
            }

            snakeLocation.AddFirst(newHeadLocation);
            fieldStateChanged.Invoke(newHeadLocation, FieldState.SnakeHead, snakeHeadDirection);

            if (FieldContainsFruit(newHeadLocation))
            {
                GenerateNewFruit();
                Score = Score + 1;
            }
            else
            {
                var snakeTail = snakeLocation.Last();
                snakeLocation.RemoveLast();
                fieldStateChanged.Invoke(snakeTail, FieldState.Empty, null);
            }
        }

        private bool FieldIsLosing(Tuple<int, int> field)
        {
            if (field.Item1 < 0 || field.Item1 >= boardRows || field.Item2 < 0 || field.Item2 >= boardCols
                || snakeLocation.Contains(field))
            {
                return true;
            }
            return false;
        }

        private bool FieldContainsFruit(Tuple<int, int> field)
        {
            return fruitLocation.Equals(field);
        }

        public void GenerateNewFruit()
        {
            bool correct = false;
            do
            {
                var randomNumberGenerator = new Random();
                int row = randomNumberGenerator.Next(boardRows);
                int col = randomNumberGenerator.Next(boardCols);
                Tuple<int, int> randomField = new Tuple<int, int>(row, col);
                if (!snakeLocation.Contains(randomField))
                {
                    fruitLocation = randomField;
                    fieldStateChanged?.Invoke(fruitLocation, FieldState.Fruit, null);
                    correct = true;
                }
            } while (!correct);
        }
    }
}
