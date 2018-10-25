using TickTackToe.Interfaces;

namespace TickTackToe
{
    public class LocationOnBoard: ILocationOnBoard
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool BelongsToSuccessRow { get; set; }

        public PlayerId? TakenBy { get; private set; }

        public LocationOnBoard(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool TryTakeLocation(PlayerId id)
        {
            if (TakenBy == null)
            {
                TakenBy = id;
                return true;
            }
            return false;
        }
    }
}

