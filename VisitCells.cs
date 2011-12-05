namespace Life2
{
    class VisitCells
    {
        public static Cell DepthFirstVisit(Cell cell)
        {
            cell.Visited = true;
            Cell newCell = cell.Next();

            foreach (Cell neighbor in cell.Neighbors)
            {
                VisitIfNotYetVisited(newCell, neighbor);
            }

            return newCell;
        }

        private static void VisitIfNotYetVisited(Cell newCell, Cell neighbor)
        {
            if (!neighbor.Visited)
            {
                Visit(newCell, neighbor);
            }
        }

        private static void Visit(Cell newCell, Cell neighbor)
        {
            neighbor.Visited = true;
            newCell.RemoveNeighbor(neighbor);
            newCell.AddNeighbor(DepthFirstVisit(neighbor.Next()));
        }
    }
}
