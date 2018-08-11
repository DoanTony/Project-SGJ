using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class PlayerControllerSystem : ComponentSystem {

    struct Controller
    {
        public readonly int Length;
        public ComponentArray<PlayerControllerComponent> PlayerControllerComponent;
        public ComponentArray <Rigidbody2D> Rigidbody;
        public ComponentArray<Transform> Transform;
    }

    [Inject] Controller _Controllers;

    protected override void OnUpdate()
    {
        for (int i = 0; i < _Controllers.Length; i++)
        {
            PlayerControllerModel controllerModel = new PlayerControllerModel(
                _Controllers.PlayerControllerComponent[i], 
                _Controllers.Transform[i],
                _Controllers.Rigidbody[i]);

            Move(controllerModel);
        }
    }

    private void Move(PlayerControllerModel _controllerModel)
    {
        PlayerControllerObject controller = _controllerModel.Component.controller;
        float xMovement = Input.GetAxis("Horizontal") * controller.movementSpeed * Time.deltaTime;
        float yMovement = Input.GetAxis("Vertical") * controller.movementSpeed * Time.deltaTime;
        Vector2 movements = new Vector2(xMovement, yMovement);
        _controllerModel.Rigidbody.AddForce(movements,ForceMode2D.Impulse);
    }
}

#region PlayerControllerModel

struct PlayerControllerModel
{
    public PlayerControllerComponent Component;
    public Transform Transform;
    public Rigidbody2D Rigidbody;

    public PlayerControllerModel(PlayerControllerComponent _component, Transform _transform, Rigidbody2D _rigidbody)
    {
        Component = _component;
        Transform = _transform;
        Rigidbody = _rigidbody;
    }
}

#endregion
