using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private Grid<GridCell> grid;
    private List<GridCell> openList;
    private List<GridCell> closedList;

    public static Pathfinding Instance;
    public Pathfinding(int width, int height)
    {
        grid = new Grid<GridCell>(width, height, 10f, new Vector3(-19, -5), (Grid<GridCell> g, int x, int y) => new GridCell(g,x, y));
        Instance = this;

    }
    public Grid<GridCell> GetGrid()
    {
        return this.grid;
    }
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<GridCell> path = FindPath(startX, startY, endX, endY);

        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(GridCell cell in path)
            {
                vectorPath.Add(new Vector3(cell.x, cell.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }
    public List<GridCell> FindPath(int startX, int startY, int endX, int endY)
    {
        GridCell startNode = grid.GetGridObject(startX, startY);
        GridCell endNode = grid.GetGridObject(endX, endY);

        openList = new List<GridCell> { startNode};
        closedList = new List<GridCell>();

        for (int x = 0; x< grid.GetWidth(); x++)
        {
            for (int y = 0; y< grid.GetHeight(); y++)
            {
                GridCell gridCell = grid.GetGridObject(x, y);
                gridCell.gCost = int.MaxValue;
                gridCell.CalculateFCost();
                gridCell.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            GridCell currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(GridCell neighbor in GetNeighborList(currentNode))
            {
                if (closedList.Contains(neighbor)) continue;
                if (!neighbor.isWalkable)
                {
                    closedList.Add(neighbor);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbor);
                if (tentativeGCost < neighbor.gCost)
                {
                    neighbor.cameFromNode = currentNode;
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = CalculateDistanceCost(neighbor, endNode);
                    neighbor.CalculateFCost();

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
        //no Path
        return null;

    }

    private List<GridCell> CalculatePath(GridCell end)
    {
        List<GridCell> path = new List<GridCell>();

        path.Add(end);
        GridCell currentNode = end;

        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(GridCell a, GridCell b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance-yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private GridCell GetLowestFCostNode(List<GridCell> gridCellList)
    {
        GridCell lowestFCostNode = gridCellList[0];
        for (int i = 1; i < gridCellList.Count; i++)
        {
            if (gridCellList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = gridCellList[i];
            }
        }
        return lowestFCostNode;
    }

    private List<GridCell> GetNeighborList(GridCell current)
    {
        List<GridCell> neighborList = new List<GridCell>();

        if (current.x - 1 >= 0)
        {
            neighborList.Add(grid.GetGridObject(current.x - 1, current.y));

            if (current.y - 1 >= 0) neighborList.Add(grid.GetGridObject(current.x - 1, current.y - 1));
            if (current.y + 1 < grid.GetHeight()) neighborList.Add(grid.GetGridObject(current.x - 1, current.y + 1));

        }
        if (current.x + 1 < grid.GetWidth())
        {
            neighborList.Add(grid.GetGridObject(current.x + 1, current.y));
            if (current.y - 1 >= 0) { neighborList.Add(grid.GetGridObject(current.x + 1, current.y - 1)); }
            if (current.y + 1 < grid.GetHeight()) { neighborList.Add(grid.GetGridObject(current.x + 1, current.y + 1)); }
        }
        if (current.y - 1 >= 0) neighborList.Add(grid.GetGridObject(current.x, current.y - 1));
        if (current.y + 1 < grid.GetHeight()) neighborList.Add(grid.GetGridObject(current.x, current.y + 1));

        return neighborList;
    }
}
