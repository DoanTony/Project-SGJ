using UnityEngine;
using Unity.Entities;

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
        string horizontalAxe = _controllerModel.Component.controller.horizontalAxe;
        string verticalAxe = _controllerModel.Component.controller.verticalAxe;
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

        if (_controllerModel.Component.isDashing && !_controllerModel.Component.isDashOnCooldown)
        {
            if (!_controllerModel.Component.isReverseDash)
            {
                 xAxis = Input.GetAxisRaw(horizontalAxe);
                 yAxis = Input.GetAxisRaw(verticalAxe);
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
        _controllerModel.Rigidbody.drag = _controllerModel.Component.dashDrag;

    }

    private void Move(PlayerControllerModel _controllerModel)
    {
        PlayerControllerPropsObject controller = _controllerModel.Component.controllerProps;
            string horizontalAxe = _controllerModel.Component.controller.horizontalAxe;
            string verticalAxe = _controllerModel.Component.controller.verticalAxe;
            float xAxis = Input.GetAxisRaw(horizontalAxe) * controller.movementSpeed * Time.deltaTime;
            float yAxis = Input.GetAxisRaw(verticalAxe) * controller.movementSpeed * Time.deltaTime;
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
