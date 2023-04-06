using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Level
{
    private int _maxWidth;
    private int _maxHeight;
    

    public Grid<GridCell<bool>> Grid { get; private set; }

    public Level level;
    public Level(int maxWidth, int maxHeight)
    {
        _maxHeight = maxHeight;
        _maxWidth = maxWidth;
        Grid = new Grid<GridCell<bool>>(maxWidth, maxHeight, InitializeLevelCell);
    }

    private GridCell<bool> InitializeLevelCell(int x, int y)
    {
        return new GridCell<bool>(x, y, true);
    }

    public void movableArea(int cellsToRemove)
    {
        var playerPosition = new Vector2Int(_maxWidth / 2, _maxHeight /2);

        while (cellsToRemove > 0)
        {
            var randomDirection = RandomUtils.GetRandomEnumValue<Direction>();
            var newPlayerPosition = playerPosition + randomDirection.ToCooords();

            if (!Grid.AreCoordsValid(newPlayerPosition, true)) continue;
            if (Grid.AreCoordsValid(newPlayerPosition)) {
                Grid.Get(newPlayerPosition).Value = false;
                Grid.Get(newPlayerPosition).isWalkable = false;
                cellsToRemove--;
            }
            playerPosition = newPlayerPosition;
        }
    }

    public void Shrink()
    {
        var _emptyCells = Grid.Cells.Where(c => !c.Value).ToArray();
        var minX = _emptyCells.Min(c => c.X);
        var maxX = _emptyCells.Max(c => c.X);
        var shrinkWidth = maxX - minX + 21;
        var minY = _emptyCells.Min(c => c.Y); 
        var maxY = _emptyCells.Max(c => c.Y);
        var shrinkHeight = maxY - minY + 10;

        var newGrid = new Grid<GridCell<bool>>(shrinkWidth, shrinkHeight, InitializeLevelCell);
        for (var x = minX -1; x <= maxX; x++)
        {
            for (var y = minY -1; y <= maxY; y++)
            {
                var value = Grid.Get(x, y).Value;
                newGrid.Get(x - minX + 1, y - minY + 1).Value = value;
            }
        }
        Grid = newGrid;

    }
}
