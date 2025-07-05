using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] private Transform _weaponPoint;
        [Space]
        [SerializeField] private Vector3 _recoilRotationDirection;
        [SerializeField] private float _recoilStrength;
        [SerializeField] private float _randomRecoilStrength;
        [SerializeField] private float _recoilReduction;
        [Space]
        [SerializeField] private Vector3 _moveRecoilDirection;
        [SerializeField] private float _moveRecoilStrength;
        [SerializeField] private float _moveRandomRecoilStrength;
        [SerializeField] private float _moveRecoilReduction;

        private Quaternion _weaponDefaultRotation;
        private Quaternion _weaponCurrentRotation;
        private Vector3 _weaponDefaultPosition;
        private Vector3 _weaponCurrentPosition;

        public void Initialize()
        {
            _weaponDefaultRotation = _weaponPoint.localRotation;
            _weaponDefaultPosition = _weaponPoint.localPosition;
            ServiceLocator.GetService<Player>().GetComponent<ShootHandler>().OnShoot.AddListener(AddRecoil);
        }

        public void Update()
        {
            Recoil(Time.deltaTime);
        }

        private void AddRecoil()
        {
            _weaponCurrentRotation.eulerAngles += _recoilRotationDirection.normalized * _recoilStrength + Random.insideUnitSphere * _randomRecoilStrength;
            _weaponCurrentPosition += _moveRecoilDirection.normalized * _moveRecoilStrength + Random.insideUnitSphere * _moveRandomRecoilStrength;
        }

        private void Recoil(float interpolation)
        {
            _weaponCurrentRotation = Quaternion.Slerp(_weaponCurrentRotation, _weaponDefaultRotation, _recoilReduction * interpolation); ;
            _weaponPoint.localRotation = _weaponCurrentRotation;

            _weaponCurrentPosition = Vector3.Slerp(_weaponCurrentPosition, _weaponDefaultPosition, _moveRecoilReduction * interpolation);
            _weaponPoint.localPosition = _weaponCurrentPosition;
        }
    }
}