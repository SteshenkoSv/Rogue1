using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LevelGenerator levelGenerator;

    public List<GameObject> objectsListForComplete = new List<GameObject>();

    private void Start()
    {
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            objectsListForComplete.Add(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")) 
        {
            objectsListForComplete.Add(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Chest"))
        {
            objectsListForComplete.Add(go);
        }
    }

    private void Update()
    {
        if (objectsListForComplete.Count == 0)
        {
            levelGenerator.GenerateMap();
            Destroy(gameObject);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy" || collision.tag == "Chest")
        {
            objectsListForComplete.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
