using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TickTackToe;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TestBoard
    {
        [TestMethod]
        public void BoardInitialStatusIsInProgress()
        {
            ILogger<Board> logger = A.Fake<ILogger<Board>>();

            Board board = new Board(logger);

            Assert.AreEqual(GameStatus.InProgress, board.GameStatus);
            Assert.IsTrue(board.LocationBoxes.All(x => x.TakenBy == null));
            Assert.AreEqual(PlayerId.X, board.ActivePlayer);
        }

        [TestMethod]
        public void BoardShouldHaveTieStatus()
        {
            ILogger<Board> logger = A.Fake<ILogger<Board>>();

            Board board = new Board(logger);

            board.TakeLocationCommand(0, 0);
            board.TakeLocationCommand(0, 1);
            board.TakeLocationCommand(0, 2);
            board.TakeLocationCommand(1, 2);
            board.TakeLocationCommand(1, 0);
            board.TakeLocationCommand(2, 0);
            board.TakeLocationCommand(1, 1);
            board.TakeLocationCommand(2, 2);
            board.TakeLocationCommand(2, 1);

            Assert.AreEqual(GameStatus.Tie, board.GameStatus);
            Assert.IsTrue(board.LocationBoxes.All(x => x.TakenBy != null));
        }

        [TestMethod]
        public void BoardShouldHaveSuccessStatusWhenColumnCovered()
        {
            ILogger<Board> logger = A.Fake<ILogger<Board>>();

            Board board = new Board(logger);

            board.TakeLocationCommand(0, 0);
            board.TakeLocationCommand(0, 1);
            board.TakeLocationCommand(0, 2);
            board.TakeLocationCommand(1, 1);
            board.TakeLocationCommand(1, 0);
            board.TakeLocationCommand(2, 1);

            Assert.AreEqual(GameStatus.Finished, board.GameStatus);
            Assert.AreEqual(PlayerId.O, board.ActivePlayer);
            Assert.IsTrue(board.LocationBoxes.First(loc => loc.X == 0 && loc.Y == 1).BelongsToSuccessRow);
            Assert.IsTrue(board.LocationBoxes.First(loc => loc.X == 1 && loc.Y == 1).BelongsToSuccessRow);
            Assert.IsTrue(board.LocationBoxes.First(loc => loc.X == 2 && loc.Y == 1).BelongsToSuccessRow);
        }

        [TestMethod]
        public void BoardShouldHaveSuccessStatusWhenRowCovered()
        {
            ILogger<Board> logger = A.Fake<ILogger<Board>>();

            Board board = new Board(logger);

            board.TakeLocationCommand(0, 0);
            board.TakeLocationCommand(1, 0);
            board.TakeLocationCommand(2, 0);
            board.TakeLocationCommand(1, 1);
            board.TakeLocationCommand(0, 1);
            board.TakeLocationCommand(1, 2);

            Assert.AreEqual(GameStatus.Finished, board.GameStatus);
            Assert.AreEqual(PlayerId.O, board.ActivePlayer);
            Assert.IsTrue(board.LocationBoxes.First(loc => loc.X == 1 && loc.Y == 0).BelongsToSuccessRow);
            Assert.IsTrue(board.LocationBoxes.First(loc => loc.X == 1 && loc.Y == 1).BelongsToSuccessRow);
            Assert.IsTrue(board.LocationBoxes.First(loc => loc.X == 1 && loc.Y == 2).BelongsToSuccessRow);
        }

        // TODO: test diagonal covered and player x, skipped for code brevity

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BoardShouldThrowWhenNegativePositionTaken()
        {
            ILogger<Board> logger = A.Fake<ILogger<Board>>();

            Board board = new Board(logger);

            board.TakeLocationCommand(-1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BoardShouldThrowWhenPositionTakenBeyondBoardSize()
        {
            ILogger<Board> logger = A.Fake<ILogger<Board>>();

            Board board = new Board(logger);

            board.TakeLocationCommand(0, 3);
        }
    }
}


