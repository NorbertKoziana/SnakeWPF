using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class Snake
    {
        public LinkedList<Tuple<int, int>> currentLocation;
        public Direction currentDirection;

        public Snake()
        {
            this.currentLocation = new LinkedList<Tuple<int, int>>();
            currentLocation.AddFirst(new Tuple<int, int>(3, 3));
            this.currentDirection = Direction.Right;
        }

        public void MakeNextMove()//musi byc wywolywane np. co sekunde (powinno sie dac zmienic w opcjach jak czesto)
        {
            currentLocation.RemoveLast();

            Tuple<int, int> headLocation = currentLocation.First();

            Tuple<int, int> newHeadLocation = null;

            switch (currentDirection)
            {
                case Direction.Left:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1, headLocation.Item2 + 1);
                    break;
                case Direction.Right:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1, headLocation.Item2 - 1);
                    break;
                case Direction.Top:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1 + 1, headLocation.Item2);
                    break;
                default:
                    newHeadLocation = new Tuple<int, int>(headLocation.Item1 - 1, headLocation.Item2);
                    break;
            }

            //if (hasPlayerLost(newHeadLocation))
            //{
            //    return;
            //}
            //else if (hasPlayerGainedScore(newHeadLocation))
            //{
            //    generateNewFruit();
            //}

            currentLocation.AddFirst(newHeadLocation);
        }
    }
}
