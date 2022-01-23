using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator _instance;
    public static LevelGenerator Instance { get { return _instance; } }

    public Tilemap tileMapWalls;
    public Tilemap tileMapBackground;
    public Tilemap tileMapLava;
    public Tile wallTile;
    public Tile backgroundTile;
    public Tile lavaTile;

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
    public GameObject lavaPb;
    public GameObject shrinePb;

    public List<GameObject> lightsList = new List<GameObject>();

    public float lavaTimerStart = 5f;
    public float lavaTimerCurrent;
    public float lavaSpreadTime;
    private float timer = 0f;

    public Text lavaTimerText;

    private bool lavaSpawned = false;

    public List<Coroutine> lavaCoroutine = new List<Coroutine>();

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
        lavaSpawned = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f && lavaTimerCurrent > 0f) 
        {
            UpdateLavaTimer();
            timer = 0f;
        }

        if (lavaTimerCurrent <= 0f && !lavaSpawned)
        {
            RunLavaSpawn();
        }
    }

    public void GenerateMap() 
    {
        levelGo.transform.rotation = Quaternion.identity;

        tileMapLava.ClearAllTiles();
        FindPath();
        RemoveLights();
        GenerateBackground();
        GenerateWalls();
        SpawnLevelContent();
        ResetLavaTimer();
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

        PathFind.Point _from = null;
        PathFind.Point _to = null;

        while (_from == _to) 
        {
            int randFromX = Random.Range(1, mapWidth - 1);
            int randFromY = Random.Range(1, mapHeight - 1);
            _from = new PathFind.Point(randFromX, randFromY);

            int randToX = Random.Range(1, mapWidth - 1);
            int randToY = Random.Range(1, mapHeight - 1);
            do
            {
                randToX = Random.Range(1, mapWidth - 1);
                randToY = Random.Range(1, mapHeight - 1);
                _to = new PathFind.Point(randToX, randToY);
            } while (Mathf.Abs((randFromX + randFromY) - (randToX + randToY)) < mapHeight/2);
        }

        PathFind.Grid grid = new PathFind.Grid(mapWidth, mapHeight, tilesmap);

        path = PathFind.Pathfinding.FindPath(grid, _from, _to);
        path.Insert(0, _from);
    }
    
    private void AddObstacleOnPath(List<PathFind.Point> pth) 
    {
        foreach (PathFind.Point p in pth)
        {

        }
    }

    private GameObject Spawn(string objectName, Vector3 spawnPosition) 
    {
        GameObject prefabToSpawn = null;
        GameObject instance = null;

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
            case "shrine":
                prefabToSpawn = shrinePb;
                break;
        }

        if (prefabToSpawn != null)
        {
            instance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            instance.transform.parent = levelGo.transform;
        }

        return instance;
    }

    private Vector3 PathPointToWorld(PathFind.Point point) 
    {
        return new Vector3(point.x - mapWidth / 2f, point.y - mapHeight / 2f, 0);
    }

    private void RemoveLights() 
    {
        for (int i = lightsList.Count - 1; i >= 0; i--) 
        {
            Destroy(lightsList[i]);
            lightsList.RemoveAt(i);
        }
    }

    private void SpawnLevelContent()
    {
        Spawn("player", PathPointToWorld(path[0]));
        Spawn("door", PathPointToWorld(path[path.Count - 1]));

        for (int i = 2; i < path.Count - 1; i += 3)
        {
            Spawn("enemy", PathPointToWorld(path[i]));
        }

        for (int i = 3; i < path.Count - 1; i += 4)
        {
            Spawn("chest", PathPointToWorld(path[i]));
        }

        for (int i = 0; i < path.Count - 1; i += 2)
        {
            lightsList.Add(Spawn("light", PathPointToWorld(path[i])));
        }

        for (int i = 5; i < path.Count - 1; i += 5)
        {
            lightsList.Add(Spawn("shrine", PathPointToWorld(path[i])));
        }
    }

    private IEnumerator SpawnLava(PathFind.Point point, float delay)
    {
        yield return new WaitForSeconds(delay);
        //GameObject instance = Instantiate(lavaPf, pos, Quaternion.identity);
        //instance.transform.parent = levelGo.transform;
        tileMapLava.SetTile(new Vector3Int(point.x, point.y, 0), lavaTile);
    }

    private void RunLavaSpawn() 
    {
        lavaSpawned = true;
        for (int i = 0; i < path.Count; i++)
        {
            lavaCoroutine.Add(StartCoroutine(SpawnLava(path[i], lavaSpreadTime * i)));
        }
    }

    private void UpdateLavaTimer() 
    {
        lavaTimerCurrent -= 1;
    }

    private void ResetLavaTimer() 
    {
        tileMapLava.ClearAllTiles();
        tileMapLava.transform.position = new Vector3(-mapWidth / 2f, -mapHeight / 2f, 0f);

        lavaTimerCurrent = lavaTimerStart;
        lavaSpawned = false;
    }

    public void StopLava() 
    {
        if (lavaCoroutine != null)
        {
            foreach (Coroutine c in lavaCoroutine)
            {
                StopCoroutine(c);
            }
        }
        lavaCoroutine.Clear();
        Debug.Log(lavaCoroutine.Count);
    }
}
