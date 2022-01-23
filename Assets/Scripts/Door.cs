using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public IngameUI IngameUI;
    public List<GameObject> objectsListForComplete = new List<GameObject>();
    private bool isNextLevelTriggered = false;
    private void Start()
    {
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        IngameUI = GameObject.Find("IngameUI").GetComponent<IngameUI>();

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
        int nullCounter = 0;

        foreach (GameObject go in objectsListForComplete)
        {
            if (go == null)
                nullCounter++;
        }

        if (objectsListForComplete.Count == 0 || nullCounter == objectsListForComplete.Count)
        {
            if (!IngameUI.isGameOver && !isNextLevelTriggered) 
            {
                isNextLevelTriggered = true;
                StartCoroutine(NextRoom(IngameUI.levelTransitionDelay));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy" || collision.tag == "Chest" || collision.gameObject.tag == "Shrine")
        {
            objectsListForComplete.Remove(collision.gameObject);
            StartCoroutine(DestrObject(collision.gameObject));
        }
    }

    private IEnumerator NextRoom(float delay) 
    {
        levelGenerator.StopLava();

        yield return new WaitForSeconds(delay);
        levelGenerator.GenerateMap();
        levelGenerator.StopLava();
        StartCoroutine(DestrObject(gameObject));
    }

    private IEnumerator DestrObject(GameObject gO) 
    {
        yield return new WaitForSeconds(0);
        Destroy(gO);
    }
}
