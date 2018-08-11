using UnityEngine;
using System.Collections;

public class AttachComponent : MonoBehaviour
{

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
            this.transform.parent = collision.transform;
        }
    }
}
