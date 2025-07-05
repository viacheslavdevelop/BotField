using Unity.Cinemachine;
using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class ScopeHandler : MonoBehaviour
    {
        [SerializeField] private Transform _scopePoint;
        [SerializeField] private Weapon _currentWeapon;

        private CinemachineCamera _camera;
        private PlayerInput _playerInput;

        private Vector3 _targetPosition;
        private float _normalFOV;
        private float _targetFOV;

        public void Initialize()
        {
            _playerInput = ServiceLocator.GetService<PlayerInput>();
            _camera = ServiceLocator.GetService<CinemachineCamera>();
            _normalFOV = _camera.Lens.FieldOfView;
        }

        public void Update()
        {
            if (_playerInput.IsScope())
            {
                _targetPosition = _currentWeapon.ScopePosition;
                _targetFOV = _normalFOV / _currentWeapon.Scope;
            }
            else
            {
                _targetPosition = _currentWeapon.NormalPosition;
                _targetFOV = _normalFOV;
            }

            Scope(Time.deltaTime);
        }

        public void Scope(float interpolation)
        {
            _scopePoint.localPosition = Vector3.Slerp(_scopePoint.localPosition, _targetPosition, _currentWeapon.ScopeSpeed * interpolation);
            _camera.Lens.FieldOfView = Mathf.Lerp(_camera.Lens.FieldOfView, _targetFOV, _currentWeapon.ScopeSpeed * interpolation);
        }
    }
}