using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public IngameUI IngameUI;

    private void Start()
    {
        IngameUI = GameObject.Find("IngameUI").GetComponent<IngameUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy" || collision.tag == "Chest")
        {
            StartCoroutine(DestrObject(collision.gameObject));

            if (collision.tag == "Player")
                IngameUI.EndRun();
        }
    }

    private IEnumerator DestrObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
