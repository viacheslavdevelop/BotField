using UnityEngine;

namespace BotField.Gameplay.UI
{
    public class MobileUIHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _mobileCanvas;

        public void Initialize()
        {
            if (PlayerInfo.Platform != Platform.Mobile)
            {
                _mobileCanvas.gameObject.SetActive(false);
            }
        }
    }
}