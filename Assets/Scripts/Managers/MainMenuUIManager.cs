using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo;

namespace YATE
{
    public class MainMenuUIManager : Singleton<MainMenuUIManager>
    {
        public void LoadPlayLevel()
        {
            LevelManager.Instance.LoadPlayLevel();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}