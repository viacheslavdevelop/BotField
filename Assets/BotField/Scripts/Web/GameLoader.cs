using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BotField
{
    public class GameLoader : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void GameReady();

        [SerializeField] private PlayerInitializer _playerInitializer;

        private void Start()
        {
            _playerInitializer.OnLoadComplete.AddListener(StartGame);
            _playerInitializer.Initialize();
        }

        public void StartGame()
        {
            if (!Application.isEditor)
            {
                GameReady();
            }
            
            SceneManager.LoadScene(1);
        }
    }
}