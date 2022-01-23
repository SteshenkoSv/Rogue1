using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsProcessor : MonoBehaviour
{
    public Collider2D playerCollider;
    public IngameUI IngameUI;

    private void Start()
    {
        IngameUI = GameObject.Find("IngameUI").GetComponent<IngameUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Chest" || collision.gameObject.tag == "Shrine")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 2f;
            collision.isTrigger = false;
            Physics2D.IgnoreCollision(playerCollider, collision, true);

            if (collision.tag == "Enemy")
            {
                IngameUI.AddMob();
            }
            else if (collision.tag == "Chest")
            {
                IngameUI.AddChest();
            }
            else if (collision.tag == "Shrine")
            {
                IngameUI.AddShrine();
            }
        }

        if (collision.gameObject.tag == "Door")
        {
            IngameUI.AddRoom();
        }
    }
}
