using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo;
using UnityEngine.UI;

namespace YATE.UI
{
    public enum ECasonStatus
    {
        Sighted, Unsighted
    }

    public enum EBabyStatus
    {
        Panicked, Unpanicked
    }

    public class LevelUIManager : Singleton<LevelUIManager>
    {
        [SerializeField] private GameObject optionsMenu;

        [Space(10)]

        [Header("Player Status UI Elements")]

        [Space(10)]

        [SerializeField] private GameObject playerStatus;
        [SerializeField, ReadOnly] private PlayerCharacter playerCharacter;
        [SerializeField, ReadOnly] private BabyDiscomfort babyDiscomfort;
        
        [Space(10)]

        [SerializeField] private GameObject neutral;
        [SerializeField] private GameObject unsighted_babyPanicked;
        [SerializeField] private GameObject sighted_babyUnpanicked;
        [SerializeField] private GameObject sighted_babyPanicked;

        [Space(10)]

        [Header("Cut Scenes UI Elements")]

        [Space(10)]
        
        [SerializeField] private GameObject cutscenes;
        [SerializeField] private Image cutsceneImageBox;
        [SerializeField] private List<Sprite> cutsceneImages;

        [Header("Death Screen")]
        [SerializeField] private GameObject deathScreen;

        private bool optionsMenuOn;
        private int imageIndex = 0;

        private ECasonStatus currentCasonStatus;
        private EBabyStatus currentBabyStatus;

        private void OnDisable()
        {
            playerCharacter.OnSighted -= UpdateCasonStatus;
            playerCharacter.OnUnsighted -= UpdateCasonStatus;
            playerCharacter.OnDie -= OnDie;

            babyDiscomfort.OnCryingStart -= UpdateBabyStatus;
            babyDiscomfort.OnCryingStop -= UpdateBabyStatus;
        }

        public void Init(PlayerCharacter playerCharacter)
        {
            this.playerCharacter = playerCharacter;
            babyDiscomfort = playerCharacter.GetComponent<BabyDiscomfort>();

            playerCharacter.OnSighted += UpdateCasonStatus;
            playerCharacter.OnUnsighted += UpdateCasonStatus;
            playerCharacter.OnDie += OnDie;

            babyDiscomfort.OnCryingStart += UpdateBabyStatus;
            babyDiscomfort.OnCryingStop += UpdateBabyStatus;

            InitOptionsMenu();
            InitPlayerStatus();
            InitCutscenes();
        }

        // Update is called once per frame
        void Update()
        {
            HandleCutscenes();
            HandleOptionsMenu();
        }

        IEnumerator WaitForDeathScreen(float delay)
        {
            yield return new WaitForSeconds(delay);
            deathScreen.SetActive(true);
        }

        private void OnDie()
        {
            StartCoroutine(WaitForDeathScreen(5f));
        }

        public void InitDeathScreen()
        {
            deathScreen.gameObject.SetActive(false);
        }

        private void InitOptionsMenu()
        {
            optionsMenuOn = false;
            optionsMenu?.SetActive(false);
        }

        private void InitPlayerStatus()
        {
            currentBabyStatus = EBabyStatus.Unpanicked;
            currentCasonStatus = ECasonStatus.Unsighted;
            playerStatus?.SetActive(false);
        }

        private void InitCutscenes()
        {
            cutscenes.gameObject.SetActive(true);
            cutsceneImageBox.sprite = cutsceneImages[imageIndex];
        }

        public void UpdateBabyStatus(EBabyStatus babyStatus)
        {
            currentBabyStatus = babyStatus;
            UpdatePlayerStatusUI();
        }

        public void UpdateCasonStatus(ECasonStatus casonStatus)
        {
            currentCasonStatus = casonStatus;
            UpdatePlayerStatusUI();
        }

        private void UpdatePlayerStatusUI()
        {
            if (currentCasonStatus == ECasonStatus.Unsighted
                && currentBabyStatus == EBabyStatus.Unpanicked)
            {
                neutral.SetActive(true);
                unsighted_babyPanicked.SetActive(false);
                sighted_babyUnpanicked.SetActive(false);
                sighted_babyPanicked.SetActive(false);
                return;
            }

            if (currentCasonStatus == ECasonStatus.Unsighted
                && currentBabyStatus == EBabyStatus.Panicked)
            {
                neutral.SetActive(false);
                unsighted_babyPanicked.SetActive(true);
                sighted_babyUnpanicked.SetActive(false);
                sighted_babyPanicked.SetActive(false);
                return;
            }

            if (currentCasonStatus == ECasonStatus.Sighted
                && currentBabyStatus == EBabyStatus.Unpanicked)
            {
                neutral.SetActive(false);
                unsighted_babyPanicked.SetActive(false);
                sighted_babyUnpanicked.SetActive(true);
                sighted_babyPanicked.SetActive(false);
                return;
            }

            if (currentCasonStatus == ECasonStatus.Sighted
                && currentBabyStatus == EBabyStatus.Panicked)
            {
                neutral.SetActive(false);
                unsighted_babyPanicked.SetActive(false);
                sighted_babyUnpanicked.SetActive(false);
                sighted_babyPanicked.SetActive(true);
                return;
            }
        }

        private void HandleOptionsMenu()
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

        private void HandleCutscenes()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                imageIndex++;

                if (imageIndex > cutsceneImages.Count - 1)
                {
                    cutscenes.gameObject.SetActive(false);

                    UpdatePlayerStatusUI();
                    playerStatus.gameObject.SetActive(true);
                    return;
                }

                cutsceneImageBox.sprite = cutsceneImages[imageIndex];
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

        public void MainMenuButton()
        {
            GameManager.Instance.LoadMainMenu();
        }

        public void ExitButton()
        {
            GameManager.Instance.ExitGame();
        }

        public void RetryButton()
        {
            GameManager.Instance.LoadCurrentLevel();
        }
    }
}