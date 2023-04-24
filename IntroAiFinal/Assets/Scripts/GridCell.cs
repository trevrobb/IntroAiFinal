using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCell
{
    public Grid<GridCell> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;

    public GridCell cameFromNode;
    public GridCell(Grid<GridCell> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
        
        
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void setTiles()
    {
       
    }
}
