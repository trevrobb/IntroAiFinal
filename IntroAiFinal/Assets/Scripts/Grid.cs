using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Grid <T>
{
    public T[,]gridArray;
    public int Width { get; }
    public int Height { get; }

    private float cellSize;
    private Vector3 originPosition;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createGrid)
    {
        this.Width = width;
        this.Height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new T[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGrid(this, x, y);
            }
        }
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
    }

    
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-originPosition).y / cellSize);
    }
    public void SetValue(Vector3 worldPosition, T value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    
    public void SetValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x<Width && y<Height)
        {
            gridArray[x, y] = value;
        }
        
    }

    public T GetGridObject(int x, int y)    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(T);
        }
    }
    public T GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    public int GetWidth()
    {
        return this.Width;
    }

    public int GetHeight()
    {
        return this.Height;
    }

    public float GetCellSize()
    {
        return this.cellSize;
    }
 
}
