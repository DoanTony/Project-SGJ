using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GameObjectEntity))]
public class FloatingComponent : MonoBehaviour {

    public FloatingObject floatingObject;
    public Collider2D collision;
    public Collider2D trigger;

    private Rigidbody2D rb;

    private void Awake()
    {
        collision.enabled = false;
        trigger.enabled = false;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

   private void Launch()
    {
        Vector2 launchDirection = new Vector2(Random.Range(4, 6) * (Random.Range(-1,1) *2 -1), Random.Range(4, 6) * (Random.Range(-1, 1) * 2 - 1));
        rb.AddForce(launchDirection, ForceMode2D.Impulse);
        StartCoroutine(EnableCollision());
    }

    private IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.5f);
        collision.enabled = true;
        trigger.enabled = true;
    }
}
