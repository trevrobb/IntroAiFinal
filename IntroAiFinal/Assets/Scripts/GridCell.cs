using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class GridCell<T>
{
    public int X { get; }
    public int Y { get; }
    public T Value { get; set; }

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;

    public GridCell<T> cameFromNode;
    public GridCell(int x, int y, T value)
    {
        X = x;
        Y = y;
        Value = value;
        isWalkable = true;
    }

    public GridCell<T> Get(int x, int y, T value)
    {
        return new GridCell<T>(x, y, value);
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    
}
