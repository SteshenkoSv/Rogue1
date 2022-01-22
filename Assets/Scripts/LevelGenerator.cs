using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public Tilemap tileMapWalls;
    public Tilemap tileMapBackground;
    public Tile wallTile;
    public Tile backgroundTile;

    public int mapWidth;
    public int mapHeight;

    public List<WayPoints> wayPoints;

    [System.Serializable]
    public class WayPoints
    {
        public Vector2Int waypoint;
    }

    private int[,] mapLayout;

    private void Start()
    {
        mapLayout = new int[mapWidth, mapHeight];
        GenerateMap();
    }

    private void GenerateMap() 
    {
        GenerateBackground();
        GenerateWalls();
    }

    private void GenerateBackground() 
    {
        tileMapBackground.ClearAllTiles();
        tileMapBackground.transform.position = new Vector3(-mapWidth / 2f, -mapHeight / 2f, 0f);

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                tileMapBackground.SetTile(new Vector3Int(i, j, 0), backgroundTile);
                mapLayout[i, j] = 0;
            }
        }
    }

    private void GenerateWalls() 
    {
        tileMapWalls.ClearAllTiles();
        tileMapWalls.transform.position = new Vector3(-mapWidth / 2f, -mapHeight / 2f, 0f);

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                if (i == 0 || j == 0 || i == mapWidth-1 || j == mapHeight-1) 
                {
                    tileMapWalls.SetTile(new Vector3Int(i, j, 0), wallTile);
                    mapLayout[i, j] = 1;
                }
            }
        }
    }

    private void FindPath() 
    {
        //// create the tiles map
        //bool[,] tilesmap = new bool[mapWidth, mapHeight];
        //// set values here....
        //// true = walkable, false = blocking

        //// create a grid
        //PathFind.Grid grid = new PathFind.Grid(width, height, tilesmap);
    }
}
