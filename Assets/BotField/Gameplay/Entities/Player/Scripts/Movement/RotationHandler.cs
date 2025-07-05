using Unity.Cinemachine;
using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class RotationHandler : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [Space]
        [SerializeField] private float _rotationSpeed;
        [Range(-180, 180)][SerializeField] private float _minRotationAngle = -90;
        [Range(-180, 180)][SerializeField] private float _maxRotationAngle = 90;

        private CinemachineInputAxisController _axisController;
        private Transform _transform;
        private PlayerInput _playerInput;
        private float _xInput;
        private float _yInput;
        private float _xRotation;

        public void Initialize()
        {
            //CinemachineCore.GetInputAxis = GetCustomAxis;
            //_axisController = ServiceLocator.GetService<CinemachineInputAxisController>();
            _transform = transform;
            //_axisController.Controllers[0].Input.LegacyGain = _rotationSpeed;
            //_axisController.Controllers[1].Input.LegacyGain = -_rotationSpeed;
            _playerInput = ServiceLocator.GetService<PlayerInput>();
        }

        private void Update()
        {
            SetRotationDirection();
            Rotate(_xInput, _yInput, Time.deltaTime);
        }

        /*public float GetCustomAxis(string axisName)
        {
            if (axisName == "Mouse X")
            {
                return _playerInput.GetLookInput().x;
            }
            if (axisName == "Mouse Y")
            {
                return _playerInput.GetLookInput().y;
            }

            return 0;
        }*/

        public void SetRotationDirection()
        {
            _xInput = _playerInput.GetLookInput().x;
            _yInput = _playerInput.GetLookInput().y;
        }

        private void Rotate(float xInput, float yInput, float interpolation)
        {
            _transform.Rotate(xInput * _rotationSpeed * interpolation * Vector3.up);

            _xRotation -= yInput * _rotationSpeed * interpolation;
            _xRotation = Mathf.Clamp(_xRotation, _minRotationAngle, _maxRotationAngle);

            _head.localEulerAngles = new Vector3(_xRotation, 0, 0);
        }
    }
}