using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class HeadRecoil : MonoBehaviour
    {
        [SerializeField] private Transform _recoilHead;
        [Space]
        [SerializeField] private Vector3 _recoilRotationDirection;
        [SerializeField] private float _recoilStrength;
        [SerializeField] private float _randomRecoilStrength;
        [SerializeField] private float _recoilReduction;

        private Quaternion _headDefaultRotation;
        private Quaternion _headCurrentRotation;
        

        public void Initialize()
        {
            _headDefaultRotation = _recoilHead.localRotation;
            ServiceLocator.GetService<Player>().GetComponent<ShootHandler>().OnShoot.AddListener(AddRecoil);
        }

        public void Update()
        {
            Recoil(Time.deltaTime);
        }

        private void AddRecoil()
        {
            _headCurrentRotation.eulerAngles += _recoilRotationDirection * _recoilStrength + Random.insideUnitSphere * _randomRecoilStrength;
        }

        private void Recoil(float interpolation)
        {
            _headCurrentRotation = Quaternion.Slerp(_headCurrentRotation, _headDefaultRotation, _recoilReduction * interpolation);
            _recoilHead.localRotation = _headCurrentRotation;
        }
    }
}