using System.Collections.Generic;
using System.Linq;


namespace Life2
{
    internal abstract class Cell
    {
        private readonly List<Cell> _neighbors = new List<Cell>();

        public abstract Cell Next();
        public abstract bool IsAlive { get; }
        public bool Visited { get; set; }
        public List<Cell> Neighbors { get { return _neighbors; } }


        public static Cell MakeLive()
        {
            return new LiveCell();
        }

        public static Cell MakeDead()
        {
            return new DeadCell();
        }

        public Cell CloneMakeLive()
        {
            return Neighbors.Aggregate((Cell) new LiveCell { Visited = Visited }, (c, n) => c.AddUnidirectional(n));
        }

        public Cell CloneMakeDead()
        {
            return Neighbors.Aggregate((Cell) new DeadCell { Visited = Visited }, (c, n) => c.AddUnidirectional(n));
        }

        public void AddNeighbor(Cell other)
        {
            AddUnidirectional(other);
            other.AddUnidirectional(this);
        }

        public void RemoveNeighbor(Cell other)
        {
            RemoveUnidirectional(other);
            other.RemoveUnidirectional(this);
        }

        private Cell AddUnidirectional(Cell other)
        {
            _neighbors.Add(other);
            return this;
        }

        private void RemoveUnidirectional(Cell other)
        {
            _neighbors.Remove(other);
        }

        protected int CountLiveNeighbors()
        {
            return _neighbors.Count(c => c.IsAlive);
        }

    }

    internal class LiveCell : Cell
    {
        public override Cell Next()
        {
            return CountLiveNeighbors() == 2 || CountLiveNeighbors() == 3 ? CloneMakeLive() : CloneMakeDead();
        }

        public override bool IsAlive
        {
            get { return true; }
        }
    }

    internal class DeadCell : Cell
    {
        public override Cell Next()
        {
            return CountLiveNeighbors() == 3 ? CloneMakeLive() : CloneMakeDead();
        }

        public override bool IsAlive
        {
            get { return false; }
        }
    }
}