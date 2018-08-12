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
        GameObject[] cols = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in cols)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());

        }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerTrigger")
        {
            PlayerControllerComponent otherPcc = collision.transform.parent.GetComponent<PlayerControllerComponent>();
            CharacterComponent otherCc = collision.transform.parent.GetComponent<CharacterComponent>();
            if (hasTransporter)
            {
                if (otherPcc.isDashing && pcc.isDashing)
                {
                    DropTransporter();
                }
                else if(otherPcc.isDashing && !pcc.isDashing)
                {
                    pcc.isStun = true;
                    pcc.isReverseDash = true;
                    otherPcc.isDashing = false;
                    otherPcc.isStun = true;
                    otherPcc.SetPreviousVelocity();
                    pcc.previousVelocityDir = otherPcc.previousVelocityDir;
                    StartCoroutine(DelaySteal(otherCc, otherPcc));
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerTrigger")
        {
            PlayerControllerComponent otherPcc = collision.transform.parent.GetComponent<PlayerControllerComponent>();
            CharacterComponent otherCc = collision.transform.parent.GetComponent<CharacterComponent>();
            if (hasTransporter && pcc.isStun && otherPcc.currentVelocityDir != Vector2.zero)
            {
                otherPcc.SetPreviousVelocity();
                pcc.previousVelocityDir = otherPcc.currentVelocityDir;
            }
        }
    }

    private IEnumerator DelaySteal(CharacterComponent otherCc, PlayerControllerComponent otherPcc)
    {
        yield return new WaitForSeconds(0.5f);
        hasTransporter = false;
        otherCc.hasTransporter = true;
        pcc.isDashing = true;
        otherPcc.isDashing = true;
    }

    private void DropTransporter()
    {
        Instantiate(characterObject.transmiterPrefab, this.transform.position, Quaternion.Euler(Vector3.zero));
        hasTransporter = false;
    }

}
