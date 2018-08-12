using UnityEngine;
using System.Collections;

public class CharacterComponent : MonoBehaviour
{
    public CharacterObject characterObject;
    public SpriteRenderer transporter;
    public bool hasTransporter;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            if (hasTransporter)
            {
                Instantiate(characterObject.transmiterPrefab, this.transform.position, Quaternion.Euler(Vector3.zero));
                hasTransporter = false;
            }
        }
    }

}
