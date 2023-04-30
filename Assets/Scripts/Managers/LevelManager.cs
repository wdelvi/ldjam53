using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo;
using UnityEngine.SceneManagement;

namespace YATE
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Scenes")]
        [SerializeField] private string playLevel;
        [SerializeField] private string mainMenu;

        public string PlayLevel => playLevel;
        public string MainMenu => mainMenu;

        public async void LoadScene(string sceneName)
        {
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = true;
        }

        public void LoadPlayLevel()
        {
            LoadScene(playLevel);
        }

        public void LoadMainMenu()
        {
            LoadScene(mainMenu);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}