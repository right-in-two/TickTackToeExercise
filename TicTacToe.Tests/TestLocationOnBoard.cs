using Microsoft.VisualStudio.TestTools.UnitTesting;
using TickTackToe;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TestLocationOnBoard
    {
        [TestMethod]
        public void TryTakeLocationShouldBeSuccessful()
        {
            LocationOnBoard location = new LocationOnBoard(0, 0);

            bool isSuccess = location.TryTakeLocation(PlayerId.O);

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(PlayerId.O, location.TakenBy);
        }

        [TestMethod]
        public void TryTakeLocationShouldBePossibleOnlyOnce()
        {
            LocationOnBoard location = new LocationOnBoard(0, 0);

            bool isSuccess = location.TryTakeLocation(PlayerId.O);

            Assert.IsTrue(isSuccess);

            isSuccess = location.TryTakeLocation(PlayerId.X);

            Assert.IsFalse(isSuccess);
            Assert.AreEqual(PlayerId.O, location.TakenBy);
        }
    }
}
