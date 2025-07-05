using System;
using UnityEngine;

namespace BotField.Gameplay.Player
{
    public abstract class PlayerInput : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract Vector2 GetLookInput();
        public abstract Vector3 GetMovementInput();
        public abstract bool IsJump();
        public abstract bool IsShoot();
        public abstract bool IsScope();
        public abstract bool IsReload();
    }
}