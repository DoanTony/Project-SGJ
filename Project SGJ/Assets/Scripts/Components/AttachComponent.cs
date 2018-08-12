using UnityEngine;
using System.Collections;

public class AttachComponent : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canPickUp;

    private void Start()
    {
        canPickUp = false;
        rb = transform.GetComponent<Rigidbody2D>();
        Collider2D col = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
        StartCoroutine(DelayPickUp());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canPickUp)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<CharacterComponent>().hasTransporter = true;
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator DelayPickUp()
    {
        yield return new WaitForSeconds(1.5f);
        canPickUp = true;
    }
}

