using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo;

namespace YATE
{
    public class LevelUIManager : Singleton<LevelUIManager>
    {
        [SerializeField] private GameObject optionsMenu;

        private bool optionsMenuOn;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (optionsMenuOn)
                {
                    ToggleOptionsMenu(false);
                }
                else
                {
                    ToggleOptionsMenu(true);
                }
            }
        }

        public void ToggleOptionsMenu(bool state)
        {
            optionsMenuOn = state;
            optionsMenu?.SetActive(state);
        }

        public void ContinueButton()
        {
            ToggleOptionsMenu(false);
        }

        public void PlayButton()
        {
            LevelManager.Instance.LoadPlayLevel();
        }

        public void MainMenuButton()
        {
            LevelManager.Instance.LoadMainMenu();
        }

        public void ExitButton()
        {
            LevelManager.Instance.ExitGame();
        }
    }
}