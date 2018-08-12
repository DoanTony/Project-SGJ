using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GameObjectEntity))]
public class FloatingComponent : MonoBehaviour {

    public FloatingObject floatingObject;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

   private void Launch()
    {
        Vector2 launchDirection = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        rb.AddForce(launchDirection, ForceMode2D.Impulse);
    }
}
