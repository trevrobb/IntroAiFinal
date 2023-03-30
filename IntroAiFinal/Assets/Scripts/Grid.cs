using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Grid <T>
{
    public T[] Cells;
    public int Width { get; }
    public int Height { get; }

    public delegate T InitFunction(int x, int y);
    public Grid(int width, int height)
    {
        Cells = new T[width * height];
        Width = width;
        Height = height;
    }

    public Grid(int width, int height, InitFunction init) : this(width, height)
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Set(x, y, init(x, y));
            }
        }
        
    }

    public int CoordsToIndex(int x, int y)
    {
        return y * Width + x;
    }

    public Vector2Int indexToCoords(int index)
    {
        return new Vector2Int(index % Width, index / Width);
    }

    public int CoordsToIndex(Vector2Int coords)
    {
        return CoordsToIndex(coords.x, coords.y);
    }

    public void Set(int x, int y, T value)
    {
        Cells[CoordsToIndex(x, y)] = value;
    }

    public void Set(Vector2Int coords, T value)
    {
        Cells[CoordsToIndex(coords.x, coords.y)] = value;
    }

    public void Set(int index, T value)
    {
        Cells[index] = value;
    }

    public T Get(int x, int y)
    {
        return Cells[CoordsToIndex(x, y)];
    }

    public T Get(Vector2Int coords)
    {
       return Cells[CoordsToIndex(coords.x, coords.y)];
    }

    public T Get(int index)
    {
        return Cells[index];
    }

    public bool AreCoordsValid(int x, int y, bool safeWalls = false)
    {
        return safeWalls ? (x > 1 && x < Width -2 && y > 1 && y < Height -2) : 
             (x >= 0 && y >= 0 && x < Width && y < Height);
    }

    public bool AreCoordsValid(Vector2Int coords, bool safeWalls = false)
    {
        return AreCoordsValid(coords.x, coords.y, safeWalls);
    }

    public Vector2Int GetCoords(T value)
    {
        var i = Array.IndexOf(Cells, value);

        if (i == -1)
        {
            throw new ArgumentException();
        }
        return indexToCoords(i);
    }

    public List<T> GetNeighbors(Vector2Int coords, bool safeWalls = false)
    {
        var directions = (Direction[]) Enum.GetValues(typeof(Direction));
               
        
        var neighbors = new List<T>();

        foreach (var direction in directions)
        {
            var neighborCoords = coords + direction.ToCooords();
            if (AreCoordsValid(neighborCoords, safeWalls))
            {
                neighbors.Add(Get(coords));
            }
        }
        return neighbors;
    }
    public List<T> GetBorders(Vector2Int coords, bool safeWalls = false)
    {
        var directions = (Direction[])Enum.GetValues(typeof(Direction));
        var borders = new List<T>();

        foreach (var direction in directions)
        {
            var borderCoords = coords + direction.ToCooords();
            if (!AreCoordsValid(borderCoords, safeWalls))
            {
                borders.Add(Get(coords));
            }
        }
        return borders;
    }
}
