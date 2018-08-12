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
        if ((Input.GetButtonDown(_controller.controller.dash) || Input.GetButtonDown(_controller.controller.dashJoyStick)) && !_controller.isStun && !_controller.isReverseDash)
        {
            _controller.isDashing = true;
            Debug.Log( _controller.isDashing);
        }
    }

    private void MoveInputs(PlayerControllerComponent _controller)
    {
        Debug.Log(Input.GetAxisRaw(_controller.controller.horizontalAxeJoystick));

        if (Input.GetAxisRaw(_controller.controller.horizontalAxe) != 0 ||
            Mathf.Round(Input.GetAxisRaw(_controller.controller.horizontalAxeJoystick)) != 0 || 
            Input.GetAxisRaw(_controller.controller.verticalAxe) != 0 ||
            Mathf.Round(Input.GetAxisRaw(_controller.controller.verticalAxeJoystick)) != 0 )
        {
            _controller.isMoving = true;
        }
        else
        {
            _controller.isMoving = false;
        }
    }

    
}
