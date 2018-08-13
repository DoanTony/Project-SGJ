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
        StartCoroutine(DelayPickUp());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canPickUp)
        {
            if (other.tag == "PlayerTrigger")
            {
                other.transform.parent.GetComponent<CharacterComponent>().hasTransporter = true;
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator DelayPickUp()
    {
        yield return new WaitForSeconds(1f);
        canPickUp = true;
    }
}

