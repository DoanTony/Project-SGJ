using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour
{

    void Awake()
    {
        this.transform.tag = "Player";
        Collider2D col = GameObject.FindGameObjectWithTag("Transmiter").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
    }

    

}
