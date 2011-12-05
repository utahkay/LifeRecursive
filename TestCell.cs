using NUnit.Framework;

namespace Life2
{
    [TestFixture]
    public class TestCell
    {
        [Test]
        public void LiveCellDiesOfLoneliness()
        {
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(0).Next().IsAlive);
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(1).Next().IsAlive);
        }

        [Test]
        public void LiveCellSurvives()
        {
            Assert.AreEqual(true, LiveCellWithLiveNeighbors(2).Next().IsAlive);
            Assert.AreEqual(true, LiveCellWithLiveNeighbors(3).Next().IsAlive);
        }

        [Test]
        public void LiveCellDiesOfOvercrowding()
        {
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(4).Next().IsAlive);
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(5).Next().IsAlive);
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(6).Next().IsAlive);
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(7).Next().IsAlive);
            Assert.AreEqual(false, LiveCellWithLiveNeighbors(8).Next().IsAlive);
        }

        [Test]
        public void DeadCellGenerates()
        {
            Assert.AreEqual(true, DeadCellWithLiveNeighbors(3).Next().IsAlive);
        }

        [Test]
        public void DeadCellStaysDead()
        {
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(0).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(1).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(2).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(4).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(5).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(6).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(7).Next().IsAlive);
            Assert.AreEqual(false, DeadCellWithLiveNeighbors(8).Next().IsAlive);
        }

        [Test]
        public void NeighborsAreSymmetric()
        {
            Cell cell = Cell.MakeLive();
            Cell anotherCell = Cell.MakeLive();
            cell.AddNeighbor(anotherCell);
            Assert.AreEqual(1, cell.Neighbors.Count);
            Assert.AreEqual(1, anotherCell.Neighbors.Count);
        }

        [Test]
        public void RemoveNeighbors()
        {
            Cell cell = Cell.MakeLive();
            Cell anotherCell = Cell.MakeLive();
            cell.AddNeighbor(anotherCell);
            anotherCell.RemoveNeighbor(cell);
            Assert.AreEqual(0, anotherCell.Neighbors.Count);
            Assert.AreEqual(0, cell.Neighbors.Count);
        }

        [Test]
        public void DepthFirstVisit()
        {
            Cell one = Cell.MakeLive();
            Cell two = Cell.MakeLive();
            Cell three = Cell.MakeLive();
            Cell four = Cell.MakeLive();
            one.AddNeighbor(three);
            one.AddNeighbor(two);
            two.AddNeighbor(four);
            var newRoot = VisitCells.DepthFirstVisit(one);
            Assert.AreEqual(true, newRoot.IsAlive);
            Assert.AreEqual(false, newRoot.Neighbors[0].IsAlive);
            Assert.AreEqual(true, newRoot.Neighbors[1].IsAlive);
            Assert.AreEqual(false, newRoot.Neighbors[1].Neighbors[1].IsAlive);
        }

        private Cell LiveCellWithLiveNeighbors(int numLiveNeighbors)
        {
            return AddLiveNeighbors(Cell.MakeLive(), numLiveNeighbors);
        }


        private Cell DeadCellWithLiveNeighbors(int numLiveNeighbors)
        {
            return AddLiveNeighbors(Cell.MakeDead(), numLiveNeighbors);
        }

        private Cell AddLiveNeighbors(Cell cell, int numLiveNeighbors)
        {
            for (int i = 0; i < numLiveNeighbors; i++)
            {
                cell.AddNeighbor(Cell.MakeLive());
            }
            return cell;
        }
    }

}
