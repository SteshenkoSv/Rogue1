using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsProcessor : MonoBehaviour
{
    public Collider2D playerCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Chest")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 2f;
            collision.isTrigger = false;
            Physics2D.IgnoreCollision(playerCollider, collision, true);
        }
    }
}
