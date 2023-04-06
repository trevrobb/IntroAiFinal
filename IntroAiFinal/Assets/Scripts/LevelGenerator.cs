using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    //Tilemaps
    [SerializeField] Tilemap _wallsTileMap;
    [SerializeField] Tilemap _groundTileMap;
    [SerializeField] Tilemap _chestTileMap;
    //Tilebases
    [SerializeField] TileBase[] _chestTiles;
    [SerializeField] TileBase[] _wallTiles;
    [SerializeField] TileBase[] _groundTiles;
    //perameters
    [SerializeField] public int maxWidth;
    [SerializeField] public int maxHeight;

    [SerializeField] int _cellsToRemove;

    

    
    [ContextMenu("Generate Level")]

    public void Generate()
    {
        var level = new Level(maxWidth, maxHeight);
        level.movableArea(_cellsToRemove);
        level.Shrink();
        _wallsTileMap.ClearAllTiles();
        _groundTileMap.ClearAllTiles();
        _chestTileMap.ClearAllTiles();
        
        
        foreach (var cell in level.Grid.Cells)
        {

            if (cell.Value)
            {
                var _randomGround = _groundTiles[Random.Range(0, _groundTiles.Length)];
                _groundTileMap.SetTile(new Vector3Int(cell.X, cell.Y, 0), _randomGround);

            }
            else
            {
                var randomWall = _wallTiles[Random.Range(0, _wallTiles.Length)];
                _wallsTileMap.SetTile(new Vector3Int(cell.X, cell.Y, 0), randomWall) ;

                
            }
        }
        

    }

}
