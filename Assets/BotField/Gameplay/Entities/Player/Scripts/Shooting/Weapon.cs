using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BotField.Gameplay.Player
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public Transform BulletPoint { get; private set; }
        //[field: SerializeField] public Transform ShootPoint { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float ShootSpeed { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public int Ammo { get; private set; }
        [field: SerializeField] public float ReloadSpeed { get; private set; }
        [field: SerializeField] public float ShootDistance { get; private set; }
        [field: SerializeField] public float Scope { get; private set; }
        [field: SerializeField] public float ScopeSpeed { get; private set; }
        [field: SerializeField] public Vector3 ScopePosition { get; private set; }
        [field: SerializeField] public Vector3 NormalPosition { get; private set; }
        [Space]
        [field: SerializeField] public string _reloadingTriggerAnimName;

        public bool CanShoot => _ammoSent;
        public int CurrentAmmo => _currentAmmo;
        public bool IsReloading => _isReloading;

        private int _currentAmmo;
        private bool _ammoSent;
        private bool _isReloading;
        private Animator _animator;
        private Transform _camera;

        public void Initialize()
        {
            _ammoSent = true;
            _currentAmmo = Ammo;
            _animator = GetComponent<Animator>();
            _camera = ServiceLocator.GetService<Camera>().transform;
        }

        public IEnumerator Shoot()
        {
            if (_currentAmmo > 0)
            {
                _ammoSent = false;
                Vector3 startShootPoint = _camera.position;
                SubstractAmmo();
                StartCoroutine(ShootDelay());
                Bullet bullet = Instantiate(BulletPrefab, BulletPoint.position, Quaternion.identity);

                if (Physics.Raycast(startShootPoint, _camera.forward, out RaycastHit hit1, ShootDistance))
                {
                    bullet.Shot(hit1.point - BulletPoint.position, BulletSpeed);
                    float time = hit1.distance / BulletSpeed;
                    StartCoroutine(bullet.Delete(time));
                    yield return new WaitForSeconds(time);

                    if (Physics.Raycast(startShootPoint, _camera.forward, out RaycastHit hit2, ShootDistance))
                    {
                        print(hit2.transform.gameObject.name);
                    }
                }
                else 
                { 
                    bullet.Shot(_camera.forward, BulletSpeed);
                    StartCoroutine(bullet.Delete());
                }
            }
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(ShootSpeed);

            _ammoSent = true;
        }

        public void SubstractAmmo()
        {
            _currentAmmo--;
        }

        public IEnumerator Reload()
        {
            if (CurrentAmmo < Ammo && !IsReloading)
            {
                _isReloading = true;
                _animator.SetTrigger(_reloadingTriggerAnimName);
                print(_animator.GetCurrentAnimatorClipInfo(0).Length);
                yield return new WaitForSeconds(ReloadSpeed);
                _currentAmmo = Ammo;
                _isReloading = false;
            }
        }
    }
}