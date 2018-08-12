using UnityEngine;
using System.Collections;

public class CharacterComponent : MonoBehaviour
{
    public PlayerObject playerObject;
    public SpriteRenderer transporter;
    [HideInInspector] public bool hasTransporter;
    private PlayerControllerComponent pcc;
    [SerializeField] Collider2D selfCollision;
    [HideInInspector] public bool isStunSteal = false;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer characterSprite;
    [HideInInspector] public ParticleSystem dashParticles;

    private void Start()
    {
        pcc = GetComponent<PlayerControllerComponent>();
        GameObject character = Instantiate(playerObject.selectedCharacter.characterPrefab, this.transform);
        animator = character.GetComponent<Animator>();
        characterSprite = character.GetComponent<SpriteRenderer>();
        GameObject particle = Instantiate(playerObject.selectedCharacter.particle.gameObject, this.transform);
        dashParticles = particle.GetComponent<ParticleSystem>();
        dashParticles.Stop();
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
            Collider2D otherCol = collision.transform.parent.GetComponent<Collider2D>();
            if (hasTransporter)
            {
                if (otherPcc.isDashing && pcc.isDashing && !isStunSteal)
                {
                    DropTransporter();
                }
                else if(otherPcc.isDashing && !pcc.isDashing)
                {
                    Physics2D.IgnoreCollision(otherCol, selfCollision, true);
                    pcc.isStun = true;
                    pcc.isReverseDash = true;
                    isStunSteal = true;
                    otherCc.isStunSteal = true;
                    otherPcc.isDashing = false;
                    otherPcc.isStun = true;
                    otherPcc.SetPreviousVelocity();
                    pcc.previousVelocityDir = otherPcc.previousVelocityDir;
                    StartCoroutine(DelaySteal(otherCc, otherPcc));
                    StartCoroutine(DelayEnableCollision(otherCol, selfCollision, otherCc));
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

    private IEnumerator DelayEnableCollision(Collider2D col1, Collider2D col2, CharacterComponent otherCc)
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(col1, col2, false);
        isStunSteal = true;
        otherCc.isStunSteal = true;
    }

    private void DropTransporter()
    {
        Instantiate(playerObject.selectedCharacter.transmiterPrefab, this.transform.position, Quaternion.Euler(Vector3.zero));
        hasTransporter = false;
    }

}
