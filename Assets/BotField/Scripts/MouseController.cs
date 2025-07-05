using UnityEngine;

namespace BotField
{
    public static class MouseController
    {
        public static void LockMouse()
        {
            if (PlayerInfo.Platform == Platform.Desktop)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public static void UnlockMouse()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}