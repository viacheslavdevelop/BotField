using UnityEngine;

namespace BotField.Gameplay
{
    public class GameStateHandler : MonoBehaviour
    {
        public void Initialize()
        {
            MouseController.LockMouse();
        }
    }
}