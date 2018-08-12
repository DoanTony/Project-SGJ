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
            Dash(controllerModel);
        }
    }

    private void Dash(PlayerControllerModel _controllerModel)
    {
        if (_controllerModel.Component.isDashing && !_controllerModel.Component.isDashOnCooldown)
        {
          _controllerModel.Component.dashDrag = _controllerModel.Component.controller.dashDrag;
          Vector2 dashDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
           _controllerModel.Rigidbody.velocity += dashDirection * _controllerModel.Component.controller.dashForce;
            _controllerModel.Component.CooldownDashReset();
        }
        _controllerModel.Rigidbody.drag = _controllerModel.Component.dashDrag;

    }

    private void Move(PlayerControllerModel _controllerModel)
    {
        PlayerControllerObject controller = _controllerModel.Component.controller;
        float xMovement = Input.GetAxisRaw("Horizontal") * controller.movementSpeed * Time.deltaTime;
        float yMovement = Input.GetAxisRaw("Vertical") * controller.movementSpeed * Time.deltaTime;
        Vector2 movements = new Vector2(xMovement, yMovement);
        _controllerModel.Rigidbody.AddForce(movements, ForceMode2D.Impulse);
    }

}

#region PlayerControllerModel

struct PlayerControllerModel
{
    public PlayerControllerComponent Component;
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public float DashTimer;

    public PlayerControllerModel(PlayerControllerComponent _component, Transform _transform, Rigidbody2D _rigidbody)
    {
        Component = _component;
        Transform = _transform;
        Rigidbody = _rigidbody;
        DashTimer = 0f;
    }
}

#endregion
