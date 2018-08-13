using UnityEngine;
using Unity.Entities;

public class PlayerControllerSystem : ComponentSystem {

    struct Controller
    {
        public readonly int Length;
        public ComponentArray<PlayerControllerComponent> PlayerControllerComponent;
        public ComponentArray<CharacterComponent> CharacterComponent;
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
                _Controllers.CharacterComponent[i], 
                _Controllers.Transform[i],
                _Controllers.Rigidbody[i]);
            if (!controllerModel.CharacterComponent.playerObject.stopAll)
            {
                Move(controllerModel);
                Dash(controllerModel);
            }
            else
            {
                controllerModel.Rigidbody.velocity = Vector2.zero;
            }
          
        }
    }

    private void Dash(PlayerControllerModel _controllerModel)
    {
        string horizontalAxe = _controllerModel.Component.controller.horizontalAxe;
        string verticalAxe = _controllerModel.Component.controller.verticalAxe;
        string horizontalAxeJoystick = _controllerModel.Component.controller.horizontalAxeJoystick;
        string verticalAxeJoystick = _controllerModel.Component.controller.verticalAxeJoystick;
        float xAxis = 0;
        float yAxis = 0;
        float xAxisTemp = 0;
        float yAxisTemp = 0;

        if (_controllerModel.Component.isStun)
        {
            _controllerModel.Rigidbody.velocity = Vector2.zero;
        }

        if (_controllerModel.Component.isStun && _controllerModel.Component.isDashOnCooldown)
        {
            _controllerModel.Component.previousVelocityDir = _controllerModel.Rigidbody.velocity.normalized;
            _controllerModel.Component.isDashOnCooldown = false;
        }

        if ((_controllerModel.Component.isDashing && !_controllerModel.Component.isDashOnCooldown))
        {
            if (!_controllerModel.Component.isReverseDash)
            {
                if (Input.GetAxisRaw(horizontalAxe) != 0 || Input.GetAxisRaw(verticalAxe) != 0)
                {
                    xAxis = Input.GetAxisRaw(horizontalAxe);
                    yAxis = Input.GetAxisRaw(verticalAxe);
                }
                else if (Mathf.Round(Input.GetAxisRaw(horizontalAxeJoystick)) != 0 || Mathf.Round(Input.GetAxisRaw(verticalAxeJoystick)) != 0)
                {
                    xAxis = Input.GetAxisRaw(horizontalAxeJoystick);
                    yAxis = Input.GetAxisRaw(verticalAxeJoystick); 
                }
            }
             if ( xAxis == 0 && yAxis == 0 && _controllerModel.Component.isStun)
            {
                xAxisTemp = _controllerModel.Component.previousVelocityDir.x;
                yAxisTemp = _controllerModel.Component.previousVelocityDir.y;
                if (_controllerModel.Component.isReverseDash)
                {
                    xAxisTemp *= -1;
                    yAxisTemp *= -1;
                }
            }

            _controllerModel.Component.dashDrag = _controllerModel.Component.controllerProps.dashDrag;
            Vector2 dashDirection = new Vector3(xAxis + xAxisTemp, yAxis + yAxisTemp);
         
            _controllerModel.Rigidbody.velocity += dashDirection * _controllerModel.Component.controllerProps.dashForce;
            _controllerModel.Component.CooldownDashReset();
        }
        else if(_controllerModel.Component.isReverseDash && !_controllerModel.Component.isStun)
        {
            xAxisTemp = _controllerModel.Component.previousVelocityDir.x;
            yAxisTemp = _controllerModel.Component.previousVelocityDir.y;
            xAxisTemp *= -1;
            yAxisTemp *= -1;
            _controllerModel.Component.dashDrag = _controllerModel.Component.controllerProps.dashDrag;
            Vector2 dashDirection = new Vector3(xAxis + xAxisTemp, yAxis + yAxisTemp);

            _controllerModel.Rigidbody.velocity += dashDirection * _controllerModel.Component.controllerProps.dashForce;
        }
        _controllerModel.Rigidbody.drag = _controllerModel.Component.dashDrag;

    }

    private void Move(PlayerControllerModel _controllerModel)
    {
        PlayerControllerPropsObject controller = _controllerModel.Component.controllerProps;
        string horizontalAxe = _controllerModel.Component.controller.horizontalAxe;
        string verticalAxe = _controllerModel.Component.controller.verticalAxe;
        string horizontalAxeJoystick = _controllerModel.Component.controller.horizontalAxeJoystick;
        string verticalAxeJoystick = _controllerModel.Component.controller.verticalAxeJoystick;
        float xAxis = 0;
        float yAxis = 0;
        if (Input.GetAxisRaw(horizontalAxe) != 0 || Input.GetAxisRaw(verticalAxe) != 0)
        {
            xAxis = Input.GetAxisRaw(horizontalAxe) * controller.movementSpeed * Time.deltaTime;
            yAxis = Input.GetAxisRaw(verticalAxe) * controller.movementSpeed * Time.deltaTime;
            if(Input.GetAxisRaw(horizontalAxe) < 0)
            {
                _controllerModel.CharacterComponent.characterSprite.flipX = true;
            }
            else
            {
                _controllerModel.CharacterComponent.characterSprite.flipX = false;
            }
        }
        else if (Mathf.Round(Input.GetAxisRaw(horizontalAxeJoystick)) != 0 || Mathf.Round(Input.GetAxisRaw(verticalAxeJoystick)) != 0)
        {
            xAxis = Input.GetAxisRaw(horizontalAxeJoystick) * controller.movementSpeed * Time.deltaTime;
            yAxis = Input.GetAxisRaw(verticalAxeJoystick) * controller.movementSpeed * Time.deltaTime;
            if (Input.GetAxisRaw(horizontalAxeJoystick) < 0)
            {
                _controllerModel.CharacterComponent.characterSprite.flipX = true;
            }
            else
            {
                _controllerModel.CharacterComponent.characterSprite.flipX = false;
            }
        }
       
        Vector2 movements = new Vector2(xAxis, yAxis);
        _controllerModel.Component.currentVelocityDir = movements.normalized;
        if (!_controllerModel.Component.isStun)
        {
            _controllerModel.Rigidbody.AddForce(movements, ForceMode2D.Impulse);
        }
    }

}

#region PlayerControllerModel

struct PlayerControllerModel
{
    public PlayerControllerComponent Component;
    public CharacterComponent CharacterComponent;
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public float DashTimer;

    public PlayerControllerModel(PlayerControllerComponent _component, CharacterComponent  _characterComponent ,Transform _transform, Rigidbody2D _rigidbody)
    {
        Component = _component;
        CharacterComponent = _characterComponent;
        Transform = _transform;
        Rigidbody = _rigidbody;
        DashTimer = 0f;
    }
}

#endregion
