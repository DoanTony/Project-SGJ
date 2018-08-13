﻿using UnityEngine;
using System.Collections;
using Unity.Entities;
using System;

public class PlayerControllerInputSystem : ComponentSystem
{
    struct Controllers
    {
        public readonly int Length;
        public ComponentArray<PlayerControllerComponent> PlayerControllerComponent;
        public ComponentArray<CharacterComponent> CharacterComponent;
    }

    [Inject] Controllers _Controllers;

    protected override void OnUpdate()
    {
        for (int i = 0; i < _Controllers.Length; i++)
        {
            PlayerControllerComponent controller = _Controllers.PlayerControllerComponent[i];
            CharacterComponent character = _Controllers.CharacterComponent[i]; ;
            if (!character.playerObject.stopAll)
            {
                MoveInputs(controller);
                DashInputs(controller, character);
            }
        }
    }

    private void DashInputs(PlayerControllerComponent _controller, CharacterComponent _character)
    {
        if ((Input.GetButtonDown(_controller.controller.dash) || Input.GetButtonDown(_controller.controller.dashJoyStick)) && !_controller.isStun && !_controller.isReverseDash)
        {
            if (!_controller.isDashOnCooldown)
            {
                AudioBank.Instance.PlaySound(AudioBank.Instance.dashSound);
            }
            _controller.isDashing = true;
            _character.animator.SetTrigger("Dash");
            _character.dashParticles.Play();
        }
    }

    private void MoveInputs(PlayerControllerComponent _controller)
    {
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
