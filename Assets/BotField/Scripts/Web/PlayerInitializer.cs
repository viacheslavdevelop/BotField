using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace BotField
{
    public class PlayerInitializer : MonoBehaviour
    {
        public UnityEvent OnLoadComplete = new();

        [DllImport("__Internal")]
        private static extern void GetLanguage();

        [DllImport("__Internal")]
        private static extern void GetPlatform();

        [SerializeField] private Language _editorLanguage;
        [SerializeField] private Platform _editorPlatform;

        private static PlayerInitializer _instance;

        private string _language;
        private string _platform;

        public void Initialize()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (Application.isEditor)
            {
                StartInEditor();
            }
            else
            {
                StartInRuntime();
            }
        }

        private void StartInEditor()
        {
            PlayerInfo.Language = _editorLanguage;
            PlayerInfo.Platform = _editorPlatform;

            OnLoadComplete.Invoke();
        }

        private void StartInRuntime()
        {
            GetPlatform();
            GetLanguage();
            StartCoroutine(Load());
        }

        public void SetLanguage(string language)
        {
            _language = language;
        }

        public void SetPlatform(string platform)
        {
            _platform = platform;
        }

        private IEnumerator Load()
        {
            while (string.IsNullOrEmpty(_language) || string.IsNullOrEmpty(_platform))
            {
                yield return null;
            }

            PlayerInfo.Language = _language switch
            {
                "ru" => Language.Ru,
                "en" => Language.En,
                _ => Language.En,
            };

            PlayerInfo.Platform = _platform switch
            {
                "mobile" => Platform.Mobile,
                "desktop" => Platform.Desktop,
                _ => Platform.Desktop,
            };

            OnLoadComplete.Invoke();
        }
    }
}