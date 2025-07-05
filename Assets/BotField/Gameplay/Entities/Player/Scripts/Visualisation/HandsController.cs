using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class HandsController : MonoBehaviour
    {
        [SerializeField] private Transform _rightHandPoint;
        [SerializeField] private Transform _leftHandPoint;
        [SerializeField] private Transform _rightIKStart;
        [SerializeField] private Transform _leftIKStart;

        public void LateUpdate()
        {
            _rightIKStart.position = _rightHandPoint.position;
            _rightIKStart.rotation = _rightHandPoint.rotation;
            
            _leftIKStart.position = _leftHandPoint.position;
            _leftIKStart.rotation = _leftHandPoint.rotation;
        }
    }
}