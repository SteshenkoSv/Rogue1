using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objectsListForComplete;

    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectsListForComplete.Contains(collision.gameObject)) 
        {
            objectsListForComplete.Remove(collision.gameObject);
        }

        if (objectsListForComplete.Count == 0) 
        {
            gameManager.LoadNextScene();
        }
    }
}
