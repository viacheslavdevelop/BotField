using BotField.Gameplay.UI;
using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class MobileInput : PlayerInput
    {
        [SerializeField] private TouchPanel _touchPanel;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private UIButton _jumpButton;
        [SerializeField] private UIButton _shootButton;
        [SerializeField] private UIButton _scopeButton;
        [SerializeField] private UIButton _reloadButton;

        public override void Initialize()
        {
            if (PlayerInfo.Platform != Platform.Mobile)
            {
                ServiceLocator.RemoveService<MobileInput>(gameObject);
            }
        }

        public override Vector2 GetLookInput()
        {
            return new Vector2(_touchPanel.TouchInput.x, _touchPanel.TouchInput.y);
        }

        public override Vector3 GetMovementInput()
        {
            return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }

        public override bool IsJump()
        {
            return _jumpButton.IsPressed;
        }

        public override bool IsShoot()
        {
            return _shootButton.IsPressed;
        }

        public override bool IsScope()
        {
            return _scopeButton.IsPressed;
        }

        public override bool IsReload()
        {
            return _reloadButton.IsPressed;
        }
    }
}