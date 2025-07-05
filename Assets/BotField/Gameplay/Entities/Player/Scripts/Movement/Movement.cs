using UnityEngine;
using UnityEngine.InputSystem;

namespace BotField.Gameplay.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        private PlayerInput _playerInput;
        Transform _transform;

        [Header("Jumping")]
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _groundCheckPivot;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _jumpHeight = 3;
        [SerializeField] private float _groundCheckRadius = 0.5f;
        private float _velocity;
        private bool _isOnGround;

        [Header("Move")]
        [SerializeField] private float _moveSpeed;
        Vector3 _moveDirection;


        public void Initialize()
        {
            _transform = transform;
            _playerInput = ServiceLocator.GetService<PlayerInput>();
        }

        private void Update()
        {
            float interpolation = Time.deltaTime;

            _isOnGround = IsOnTheGround();

            if (_playerInput.IsJump() && _isOnGround)
            {
                Jump();
                //OnVelocityChange?.Invoke(1); для анимок
            }

            if (_isOnGround && _velocity < 0)
            {
                _velocity = -2;
                //OnVelocityChange?.Invoke(0); для анимок
            }
            else if (_velocity < 0)
            {
                //OnVelocityChange?.Invoke(-1); для анимок
            }

            SetMoveDirection();
            DoGravity(interpolation);
            Move(_moveDirection, interpolation);
        }

        public void SetMoveDirection()
        {
            _moveDirection = _playerInput.GetMovementInput(); 
        }

        private void Move(Vector3 moveDirection, float interpolation)
        {
            _controller.Move(_moveSpeed * interpolation * _transform.TransformDirection(moveDirection));
        }

        private bool IsOnTheGround()
        {
            return Physics.CheckSphere(_groundCheckPivot.position, _groundCheckRadius, _groundMask);
        }

        private void Jump()
        {
            _velocity = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }

        private void DoGravity(float interpolation)
        {
            _velocity += _gravity * interpolation;

            _controller.Move(_velocity * interpolation * Vector3.up);
        }
    }
}