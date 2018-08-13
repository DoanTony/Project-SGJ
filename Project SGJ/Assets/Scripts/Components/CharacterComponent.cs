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
    private bool canTouch = true;
    public AudioBank audioBank;
    private void Start()
    {
        pcc = GetComponent<PlayerControllerComponent>();
        GameObject character = Instantiate(playerObject.selectedCharacter.characterPrefab, this.transform);
        animator = character.GetComponent<Animator>();
        characterSprite = character.GetComponent<SpriteRenderer>();
        GameObject particle = Instantiate(playerObject.selectedCharacter.particle.gameObject, this.transform);
        dashParticles = particle.GetComponent<ParticleSystem>();
        dashParticles.Stop();
        transporter.sprite = playerObject.selectedCharacter.transporterSprite;
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            if (hasTransporter)
            {
                canTouch = false;
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
                if (otherPcc.isDashing && pcc.isDashing)
                {
                    canTouch = false;
                    DropTransporter();
                    otherCc.DropTransporter();
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
                    StopCoroutine(StartProgress());
                }
            }
        }
        if(collision.transform.tag == "Transporter")
        {
            if (canTouch)
            {
                audioBank.PlaySound(audioBank.pickupSound);
                hasTransporter = true;
                StartCoroutine(StartProgress());
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
        audioBank.PlaySound(audioBank.stealSound);
        hasTransporter = false;
        otherCc.hasTransporter = true;
        pcc.isDashing = true;
        otherPcc.isDashing = true;
        StartCoroutine(otherCc.StartProgress());
        StopCoroutine(StartProgress());
    }

    private IEnumerator DelayEnableCollision(Collider2D col1, Collider2D col2, CharacterComponent otherCc)
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(col1, col2, false);
        isStunSteal = true;
        otherCc.isStunSteal = true;
    }

    public void DropTransporter()
    {
        if (hasTransporter)
        {
            audioBank.PlaySound(audioBank.dropSound);
            hasTransporter = false;
            Instantiate(playerObject.selectedCharacter.transmiterPrefab, this.transform.position, Quaternion.Euler(Vector3.zero));
            StopCoroutine(StartProgress());
            StartCoroutine(DelayCanTouchAgain());
        }
    }

    private IEnumerator DelayCanTouchAgain()
    {
        yield return new WaitForSeconds(0.5f);
        canTouch = true;
    }

    public IEnumerator StartProgress()
    {
        while (hasTransporter)
        {
            yield return new WaitForSeconds(2);
            if (hasTransporter)
            {
                playerObject.progressBar.progress += 0.05f;
            }
        }
    }

}
