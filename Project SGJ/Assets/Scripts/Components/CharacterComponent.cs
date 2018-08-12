using UnityEngine;
using System.Collections;

public class CharacterComponent : MonoBehaviour
{
    public CharacterObject characterObject;
    public SpriteRenderer transporter;
    public bool hasTransporter;

    private PlayerControllerComponent pcc;

    private void Start()
    {
        pcc = GetComponent<PlayerControllerComponent>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            if (hasTransporter)
            {
                DropTransporter();
            }
        }
        else if (collision.transform.tag == "Player")
        {
            PlayerControllerComponent otherPcc = collision.transform.GetComponent<PlayerControllerComponent>();
            CharacterComponent otherCc = collision.transform.GetComponent<CharacterComponent>();
            if (hasTransporter)
            {
                if (otherPcc.isDashing && pcc.isDashing)
                {
                    DropTransporter();
                }
                else if(otherPcc.isDashing && !pcc.isDashing)
                {
                    hasTransporter = false;
                    otherCc.hasTransporter = true;
                }
            }
        }
    }

    private void DropTransporter()
    {
        Instantiate(characterObject.transmiterPrefab, this.transform.position, Quaternion.Euler(Vector3.zero));
        hasTransporter = false;
    }

}
