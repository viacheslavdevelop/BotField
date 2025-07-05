using UnityEngine;
using UnityEngine.Events;

namespace BotField.Gameplay.Player
{
    public class ShootHandler : MonoBehaviour
    {
        public UnityEvent OnShoot;

        [SerializeField] private Weapon _currentWeapon;

        private PlayerInput _playerInput;

        public void Initialize()
        {
            _playerInput = ServiceLocator.GetService<PlayerInput>();
            _currentWeapon.Initialize();
        }

        private void Update()
        {
            if (_playerInput.IsShoot() && _currentWeapon.CanShoot)
            {
                if (_currentWeapon.CurrentAmmo != 0 && !_currentWeapon.IsReloading)
                {
                    StartCoroutine(_currentWeapon.Shoot());
                    OnShoot?.Invoke();
                }
            }
            if (_playerInput.IsReload())
            {
                StartCoroutine(_currentWeapon.Reload());
            }
        }
    }
}