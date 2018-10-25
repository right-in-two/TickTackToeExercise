using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TickTackToe.Interfaces;

namespace TickTackToe
{
    public class Board : IBoard
    {
        private const int size = 3;

        public IEnumerable<ILocationOnBoard> LocationBoxes { get; private set; }

        public GameStatus GameStatus { get; private set; }

        public PlayerId ActivePlayer { get; private set; }

        private readonly ILogger logger;
        private readonly object lockObject = new object();

        public Board(ILogger<Board> logger)
        {
            this.logger = logger;

            ActivePlayer = PlayerId.X;

            var locationBoxes = new List<ILocationOnBoard>(size * size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    locationBoxes.Add(new LocationOnBoard(x, y));
                }
            }
            LocationBoxes = locationBoxes;
            GameStatus = GameStatus.InProgress;

            logger.Log(LogLevel.Information, "BoardCreated");
        }

        public void TakeLocationCommand(int x, int y)
        {
            logger.Log(LogLevel.Debug, "TakeLocationCommand {x} {y} {ActivePlayer}", x, y, ActivePlayer);

            ValidateLocationCoordinate(x);
            ValidateLocationCoordinate(y);

            if (GameStatus != GameStatus.InProgress)
            {
                return;
            }

            lock (lockObject)
            {
                var location = LocationBoxes.First(box => box.X == x && box.Y == y);

                if (location.TryTakeLocation(ActivePlayer))
                {
                    GameStatus = CheckGameStatus();

                    logger.Log(LogLevel.Debug, "TakeLocationCommand step executed {x} {y} {ActivePlayer} {GameStatus}", x, y, ActivePlayer, GameStatus);

                    if (GameStatus == GameStatus.InProgress)
                    {
                        ActivePlayer = ActivePlayer == PlayerId.X ? PlayerId.O : PlayerId.X;
                    }
                }
            }
        }

        private GameStatus CheckGameStatus()
        {
            // check each horizontal row
            for (int x = 0; x < size; x++)
            {
                if (CheckRowTakenByActivePlayer(box => box.X == x))
                {
                    return GameStatus.Finished;
                }
            }

            // check each vertical row
            for (int y = 0; y < size; y++)
            {
                if (CheckRowTakenByActivePlayer(box => box.Y == y))
                {
                    return GameStatus.Finished;
                }
            }

            // check diagonal row
            if (CheckRowTakenByActivePlayer(box => box.X == box.Y))
            {
                return GameStatus.Finished;
            }

            // check other diagonal row
            if (CheckRowTakenByActivePlayer(box => box.X + box.Y == size - 1))
            {
                return GameStatus.Finished;
            }

            if (LocationBoxes.All(x => x.TakenBy.HasValue))
            {
                return GameStatus.Tie;
            }

            return GameStatus.InProgress;
        }

        private bool CheckRowTakenByActivePlayer(Func<ILocationOnBoard, bool> rowSelectorPredicate)
        {
            var row = LocationBoxes.Where(rowSelectorPredicate);
            if (row.All(box => box.TakenBy == ActivePlayer))
            {
                foreach (var box in row)
                {
                    box.BelongsToSuccessRow = true;
                }
                return true;
            }
            return false;
        }

        private void ValidateLocationCoordinate(int coordinate)
        {
            if (coordinate < 0 || coordinate >= size)
            {
                throw new ArgumentOutOfRangeException($"Coordinate Out of valid range (0 to {size - 1})");
            }
        }
    }
}
