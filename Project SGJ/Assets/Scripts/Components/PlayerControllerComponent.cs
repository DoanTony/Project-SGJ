using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GameObjectEntity))]
public class PlayerControllerComponent : MonoBehaviour {

    #region Public Inspector Data

    [Tooltip("Type of controller for this character ( Player Controller Object) ")]
    public PlayerControllerObject controller;
    public PlayerControllerPropsObject controllerProps;

    #endregion

    #region Hidden Public Data From Inspector
    [HideInInspector] public float xAxis;
    [HideInInspector] public float yAxis;
    [HideInInspector] public bool dash;
    [HideInInspector] public bool isMoving;
    public bool isDashing;
    public bool isReverseDash;
    public bool isStun;
    public bool isDashOnCooldown;
    public Vector2 previousVelocityDir = Vector2.zero;
    public Vector2 currentVelocityDir = Vector2.zero;
    [HideInInspector] public float dashDrag = 1f;
    #endregion

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPreviousVelocity()
    {
        previousVelocityDir = rb.velocity.normalized;
    }

    public void CooldownDashReset()
    {
        if (!isDashOnCooldown && isDashing)
        {
            StartCoroutine(DelayDashReset());
            StartCoroutine(DelayResetDashDrag());
            isDashOnCooldown = true;
            isStun = false;
            isReverseDash = false;
        }
    }

    private IEnumerator DelayResetDashDrag()
    {
        yield return new WaitForSeconds(controllerProps.dashDragTimer);
        dashDrag = 1f;
    }

    private IEnumerator DelayDashReset()
    {
        yield return new WaitForSeconds(controllerProps.dashCooldownTimer);
        isDashOnCooldown = false;
        isDashing = false;
    }
}
