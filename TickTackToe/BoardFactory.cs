using Microsoft.Extensions.Logging;
using TickTackToe.Interfaces;

namespace TickTackToe
{
    public class BoardFactory : IBoardFactory
    {
        private ILogger<Board> logger;

        public BoardFactory(ILogger<Board> logger)
        {
            this.logger = logger;
        }

        public IBoard CreateBoard()
        {
            return new Board(logger);
        }
    }
}
