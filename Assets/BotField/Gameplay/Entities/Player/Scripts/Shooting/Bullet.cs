using System.Collections;
using UnityEngine;

namespace BotField.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _defaultDeleteTime;

        private float _speed;
        private Vector3 _direction;
        private Transform _transform;

        public void Shot(Vector3 direction, float speed)
        {
            _transform = transform;
            _direction = direction.normalized;
            _speed = speed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _transform.Translate(_speed * Time.deltaTime * _direction);
        }

        public IEnumerator Delete()
        {
            StartCoroutine(Delete(_defaultDeleteTime));
            yield return null;
        }

        public IEnumerator Delete(float deleteTime)
        {
            yield return new WaitForSeconds(deleteTime);

            gameObject.SetActive(false);
        }
    }
}