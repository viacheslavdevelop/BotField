using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] private Transform _gunPoint;
        [Space]
        [SerializeField] private float _smooth;
        [SerializeField] private float _lookSensitivity;
        [SerializeField] private float _moveSensitivity;

        private PlayerInput _playerInput;
        private float _xLook;
        private float _yLook;
        private float _xMove;
        private float _yMove;

        public void Initialize()
        {
            _playerInput = ServiceLocator.GetService<PlayerInput>();
        }

        public void Update()
        {
            SetSway();
            Sway(Time.deltaTime);
        }

        private void SetSway()
        {
            Vector2 lookInput = _playerInput.GetLookInput();
            Vector3 moveInput = _playerInput.GetMovementInput();

            _xLook = lookInput.y * _lookSensitivity;
            _yLook = lookInput.x * _lookSensitivity;

            _xMove = moveInput.x * _moveSensitivity;
            _yMove = moveInput.z * _moveSensitivity;
        }

        private void Sway(float interpolation)
        {
            Quaternion rotationX = Quaternion.AngleAxis(-_xLook + _yMove, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(_yLook, Vector3.up);
            Quaternion rotationZ = Quaternion.AngleAxis(-_yLook - _xMove, Vector3.forward);
            Quaternion targetRotation = rotationX * rotationY * rotationZ;

            _gunPoint.localRotation = Quaternion.Slerp(_gunPoint.localRotation, targetRotation, _smooth * interpolation);
        }
    }
}