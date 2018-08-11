using UnityEngine;
using System.Collections;
using Unity.Entities;
using System;

public class PlayerControllerInputSystem : ComponentSystem
{
    struct Controllers
    {
        public readonly int Length;
        public ComponentArray<PlayerControllerComponent> PlayerControllerComponent;
    }

    [Inject] Controllers _Controllers;

    protected override void OnUpdate()
    {
        for (int i = 0; i < _Controllers.Length; i++)
        {
            PlayerControllerComponent controller = _Controllers.PlayerControllerComponent[i];
            MoveInputs(controller);
            DashInputs(controller);
        }
    }

    private void DashInputs(PlayerControllerComponent _controller)
    {
        if (Input.GetButtonDown("Dash"))
        {
            _controller.isDashing = true;
        }
    }

    private void MoveInputs(PlayerControllerComponent _controller)
    {
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
        {
            _controller.isMoving = true;
        }
        else
        {
            _controller.isMoving = false;
        }
    }

    
}
