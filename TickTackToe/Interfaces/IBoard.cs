using System.Collections.Generic;

namespace TickTackToe.Interfaces
{
    public interface IBoard
    {
        IEnumerable<ILocationOnBoard> LocationBoxes { get; }
        GameStatus GameStatus { get; }
        PlayerId ActivePlayer { get; }
        void TakeLocationCommand(int x, int y);
    }
}
