using UnityEngine;
using UnityEngine.EventSystems;

namespace BotField.Gameplay.UI
{
    public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Vector2 TouchInput { get; private set; }
        private Vector2 _pointerOld;
        private int _pointerId;
        private bool _isPressed;

        private void Update()
        {
            if (_isPressed)
            {
                if (_pointerId >= 0 && _pointerId < Input.touches.Length)
                {
                    TouchInput = Input.touches[_pointerId].position - _pointerOld;
                    _pointerOld = Input.touches[_pointerId].position;
                }
                else
                {
                    TouchInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                    _pointerOld = Input.mousePosition;
                }
            }
            else
            {
                TouchInput = new Vector2();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            _pointerId = eventData.pointerId;
            _pointerOld = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}