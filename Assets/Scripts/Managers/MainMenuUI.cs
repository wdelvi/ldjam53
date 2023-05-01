using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YATE.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject options;
        [SerializeField] private GameObject controls;

        private void Start()
        {
            options.SetActive(true);
            controls.SetActive(false);
        }

        public void OpenControlsMenu()
        {
            options.SetActive(false);
            controls.SetActive(true);
        }

        public void BackButton()
        {
            controls.SetActive(false);
            options.SetActive(true);
        }

        public void PlayButton()
        {
            GameManager.Instance.LoadLevel(0);
        }

        public void ExitButton()
        {
            GameManager.Instance.ExitGame();
        }
    }
}

