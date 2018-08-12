using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/PlayerProps", order = 1)]
public class PlayerControllerPropsObject : ScriptableObject {

    [Header("Player controller props")]
    [Range(1,50)]
    public float movementSpeed = 2.0f;
    [Range(1, 50)]
    public float dashForce = 3.0f;
    [Range(0, 50)]
    public float dashCooldownTimer = 2.0f;
    [Range(1, 50)]
    public float dashDrag = 10;
    [Range(0, 2)]
    [Tooltip("How long it takes for the drag to go back to normal")]
    public float dashDragTimer = 0.5f;
}
