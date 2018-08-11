﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GameObjectEntity))]
public class PlayerControllerComponent : MonoBehaviour {

    #region Public Inspector Data

    [Header("Controller")]
    [Tooltip("Type of controller for this character ( Player Controller Object) ")]
    public PlayerControllerObject controller;

    #endregion

    #region Hidden Public Data From Inspector
    [HideInInspector] public bool isMoving;
    #endregion
}
