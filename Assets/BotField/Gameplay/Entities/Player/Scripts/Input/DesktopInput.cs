using UnityEngine;

namespace BotField.Gameplay.Player
{
    public class DesktopInput : PlayerInput
    {
        public override void Initialize()
        {
            if (PlayerInfo.Platform != Platform.Desktop)
            {
                ServiceLocator.RemoveService<DesktopInput>(gameObject);
            }
        }

        public override Vector2 GetLookInput()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public override Vector3 GetMovementInput()
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        public override bool IsJump()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                return true;
            }

            return false;
        }

        public override bool IsShoot()
        {
            if (Input.GetMouseButton(0))
            {
                return true;
            }

            return false;
        }

        public override bool IsScope()
        {
            if (Input.GetMouseButton(1))
            {
                return true;
            }

            return false;
        }

        public override bool IsReload()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                return true;
            }

            return false;
        }
    }
}
