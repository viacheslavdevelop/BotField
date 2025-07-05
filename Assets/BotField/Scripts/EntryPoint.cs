using UnityEngine;
using UnityEngine.Events;

namespace BotField
{
    public class EntryPoint : MonoBehaviour
    {
        public UnityEvent OnGameStarted = new();

        protected virtual void Awake()
        {
            OnGameStarted.Invoke();
        }
    }
}