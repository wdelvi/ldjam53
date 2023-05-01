using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo;
using UnityEngine.SceneManagement;
using YATE.UI;
using Cinemachine;

namespace YATE
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [Header("Scenes")]
        [SerializeField] private string mainMenu;
        [SerializeField] private string[] levels;

        [Header("Player Start Location")]
        [Tooltip("Disable if you want to manually place player character in scene for testing")]
        [SerializeField] private bool spawnPlayerCharacter;
        [SerializeField] private PlayerCharacter playerCharacterPrefab;
        [SerializeField] private Vector3 playerStartLocation;

        private LevelUIManager levelUIManager;
        private PlayerCharacter playerCharacter;
        private CinemachineVirtualCamera vmCam;

        public string MainMenu => mainMenu;

        private void Start()
        {
            InitPlayerCharacter();
            InitLevelUI();
            InitCamera();
        }

        private void InitPlayerCharacter()
        {
            if (spawnPlayerCharacter)
            {
                if (SceneManager.GetActiveScene().name != mainMenu
                && playerCharacter is null)
                {
                    // Instantiate player character at start location
                    playerCharacter = Instantiate(playerCharacterPrefab, playerStartLocation, Quaternion.identity);
                }
            }
            else
            {
                playerCharacter = FindObjectOfType<PlayerCharacter>();
                if (playerCharacter is null)
                {
                    Debug.LogError("If 'SpawnPlayerCharacter' not selected in GameManager, ensure a PlayerCharacter is already in scene");
                }
            }

            playerCharacter?.Init(playerStartLocation);
        }

        private void InitLevelUI()
        {
            if (playerCharacter is null) return;

            levelUIManager = FindObjectOfType<LevelUIManager>();
            levelUIManager?.Init(playerCharacter);
        }

        private void InitCamera()
        {
            if (playerCharacter is null) return;

            vmCam = FindObjectOfType<CinemachineVirtualCamera>();
            vmCam.Follow = playerCharacter.transform;
            vmCam.LookAt = playerCharacter.transform;
        }

        public async void LoadScene(string sceneName)
        {
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = true;
        }

        public void LoadLevel(int index)
        {
            LoadScene(levels[index]);
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