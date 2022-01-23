using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator _instance;
    public static LevelGenerator Instance { get { return _instance; } }

    public Tilemap tileMapWalls;
    public Tilemap tileMapBackground;
    public Tile wallTile;
    public Tile backgroundTile;

    public int mapWidth;
    public int mapHeight;

    List<PathFind.Point> path = new List<PathFind.Point>();

    private bool[,] tilesmap;

    public GameObject levelGo;

    public GameObject playerPb;
    public GameObject enemyPb;
    public GameObject chestPb;
    public GameObject lightPb;
    public GameObject doorPb;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        tilesmap = new bool[mapWidth, mapHeight];
        GenerateMap();
    }

    public void GenerateMap() 
    {
        levelGo.transform.rotation = Quaternion.identity;

        FindPath();
        GenerateBackground();
        GenerateWalls();
        Spawn("player", PathPointToWorld(path[path.Count - 1]));
        Spawn("door", PathPointToWorld(path[path.Count-2]));

        for (int i = 1; i < path.Count - 2; i+=3)
        {
            Spawn("enemy", PathPointToWorld(path[i]));
        }

        for (int i = 2; i < path.Count - 2; i += 4)
        {
            Spawn("chest", PathPointToWorld(path[i]));
        }

    }

    private void GenerateBackground() 
    {
        tileMapBackground.ClearAllTiles();
        tileMapBackground.transform.position = new Vector3(-mapWidth / 2f, -mapHeight / 2f, 0f);

        foreach (PathFind.Point p in path)
        {
            tileMapBackground.SetTile(new Vector3Int(p.x, p.y, 0), backgroundTile);
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
                int pathCrossCounter = 0;

                foreach (PathFind.Point p in path)
                {
                    if (p.x == i && p.y == j)
                        pathCrossCounter++;
                }

                if (pathCrossCounter == 0)
                {
                    tileMapWalls.SetTile(new Vector3Int(i, j, 0), wallTile);
                }
            }
        }
    }

    private void FindPath()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                tilesmap[i, j] = true;
            }
        }

        PathFind.Grid grid = new PathFind.Grid(mapWidth, mapHeight, tilesmap);

        PathFind.Point _from = null;
        PathFind.Point _to = null;

        while (_from == _to) 
        {
            int randFromX = Random.Range(1, mapWidth - 1);
            int randFromY = Random.Range(1, mapHeight - 1);
            _from = new PathFind.Point(randFromX, randFromY);

            int randToX = Random.Range(1, mapWidth - 1);
            int randToY = Random.Range(1, mapHeight - 1);
            _to = new PathFind.Point(randToX, randToY);
        }

        path = PathFind.Pathfinding.FindPath(grid, _from, _to);
        path.Add(_from);
    }
    
    private void AddObstacleOnPath(List<PathFind.Point> pth) 
    {
        foreach (PathFind.Point p in pth)
        {

        }
    }

    private void Spawn(string objectName, Vector3 spawnPosition) 
    {
        GameObject prefabToSpawn = null;

        switch (objectName) 
        {
            case "player":
                prefabToSpawn = playerPb;
                break;
            case "enemy":
                prefabToSpawn = enemyPb;
                break;
            case "chest":
                prefabToSpawn = chestPb;
                break;
            case "light":
                prefabToSpawn = lightPb;
                break;
            case "door":
                prefabToSpawn = doorPb;
                break;
        }

        if (prefabToSpawn != null)
        {
            GameObject instance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            instance.transform.parent = levelGo.transform;
        }
    }

    private Vector3 PathPointToWorld(PathFind.Point point) 
    {
        return new Vector3(point.x - mapWidth / 2f, point.y - mapHeight / 2f, 0);
    }
}
