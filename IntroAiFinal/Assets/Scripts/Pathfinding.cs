using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Pathfinding
{
    private Grid<GridCell<bool>> grid;
    private List<GridCell<bool>> openList;
    private List<GridCell<bool>> closeList;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance;

    int maxWidth = 15;
    int maxHeight = 15;
    Level level;
    
    public Pathfinding(int width, int height)
    {
        Instance = this;
        grid = new Grid<GridCell<bool>>(width, height);
        level = new Level(maxWidth, maxHeight);
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        int startIndex = level.Grid.CoordsToIndex((int)startWorldPosition.x, (int)startWorldPosition.y);
        Vector2Int startPosition = level.Grid.indexToCoords(startIndex);
        int endIndex = level.Grid.CoordsToIndex((int)endWorldPosition.x, (int)endWorldPosition.y);
        Vector2Int endPosition = level.Grid.indexToCoords(endIndex);

        List<GridCell<bool>> path = FindPath(startPosition.x, startPosition.y, endPosition.x, endPosition.y);

        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(GridCell<bool> cell in path)
            {
                vectorPath.Add(new Vector3(cell.X, cell.Y));
            }
            return vectorPath;
        }
    }

    private List<GridCell<bool>> FindPath(int startX, int startY, int endX, int endY)
    {
        GridCell<bool> startNode = level.Grid.Get(startX, startY);
        GridCell<bool> endNode = level.Grid.Get(endX, endY);

        openList = new List<GridCell<bool>> { startNode };
        closeList = new List<GridCell<bool>>();

        for (int x = 0; x < level.Grid.Width; x++)
        {
            for (int y = 0; y<level.Grid.Height; y++)
            {
                GridCell<bool> pathNode = level.Grid.Get(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateHCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            GridCell<bool> currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach (GridCell<bool> pathNode in GetNeighbors(currentNode)){
                if (closeList.Contains(pathNode))
                {
                    continue;
                }
                if (!pathNode.isWalkable)
                {
                    closeList.Add(pathNode);
                    continue;
                }
                int tentativeGCost = currentNode.gCost + CalculateHCost(currentNode, pathNode);

                if (tentativeGCost < pathNode.gCost)
                {
                    pathNode.cameFromNode = currentNode;
                    pathNode.gCost = tentativeGCost;
                    pathNode.hCost = CalculateHCost(pathNode, endNode);
                    pathNode.CalculateFCost();

                    if (!openList.Contains(pathNode))
                    {
                        openList.Add(pathNode);
                    }
                }
            }
        }
        return null;


    }
    private List<GridCell<bool>> GetNeighbors(GridCell<bool> currentNode)
    {
        List<GridCell<bool>> neighbors = new List<GridCell<bool>>();
        if (currentNode.X -1 >= 0)
        {
            //left
           neighbors.Add(level.Grid.Get(currentNode.X -1, currentNode.Y));
            if (currentNode.Y - 1 >= 0) neighbors.Add(level.Grid.Get(currentNode.X - 1, currentNode.Y - 1));
            if (currentNode.Y +1 < level.Grid.Height) neighbors.Add(level.Grid.Get(currentNode.X -1, currentNode.Y +1));
        }
        if (currentNode.X +1 < grid.Width)
        {
            neighbors.Add(level.Grid.Get(currentNode.X +1, currentNode.Y));
            if (currentNode.Y -1 >= 0) { neighbors.Add(level.Grid.Get(currentNode.X+1, currentNode.Y - 1)); }
            if (currentNode.Y +1 < level.Grid.Height) neighbors.Add (level.Grid.Get(currentNode.X+1, currentNode.Y + 1));
        }
        if (currentNode.Y -1 >= 0) neighbors.Add(level.Grid.Get(currentNode.X, currentNode.Y - 1));
        if (currentNode.Y + 1 < level.Grid.Height) neighbors.Add(level.Grid.Get(currentNode.X, currentNode.Y+1));

        return neighbors;
    }
    private List<GridCell<bool>> CalculatePath(GridCell<bool> endNode)
    {
        List<GridCell<bool>> path = new List<GridCell<bool>>();
        path.Add(endNode);

        GridCell<bool> currentNode = endNode;
        
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;

    }

    private int CalculateHCost(GridCell<bool> a, GridCell<bool> b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private GridCell<bool> GetLowestFCostNode(List<GridCell<bool>> pathNodeList)
    {
        GridCell<bool> lowestFCost = pathNodeList[0];

        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCost.fCost)
            {
                lowestFCost = pathNodeList[i];
            }
        }
        return lowestFCost;
    }
    
}
