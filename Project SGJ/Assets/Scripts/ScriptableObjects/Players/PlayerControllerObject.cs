using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player", order = 1)]
public class PlayerControllerObject : ScriptableObject {

    [Header("Player controller props`")]
    public float movementSpeed = 2.0f;
    public float dashForce = 3.0f;
    public float dashCooldownTimer = 2.0f;
}
