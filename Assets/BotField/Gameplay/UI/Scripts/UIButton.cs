using UnityEngine;

namespace BotField.Gameplay.UI
{
    public class UIButton : MonoBehaviour
    {
        public bool IsPressed { get; private set; }

        public void OnPointerDown()
        {
            IsPressed = true;
        }

        public void OnPointerUp()
        {
            IsPressed = false;
        }
    }
}