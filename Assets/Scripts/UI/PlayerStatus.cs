using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter playerCharacter;
        [SerializeField] private BabyDiscomfort babyDiscomfort;

        [Header("UI")]
        [SerializeField] private GameObject neutral;
        [SerializeField] private GameObject unsighted_babyPanicked;
        [SerializeField] private GameObject sighted_babyUnpanicked;
        [SerializeField] private GameObject sighted_babyPanicked;

        private ECasonStatus currentCasonStatus;
        private EBabyStatus currentBabyStatus;

        // Start is called before the first frame update
        void Start()
        {
            currentBabyStatus = EBabyStatus.Unpanicked;
            currentCasonStatus = ECasonStatus.Unsighted;

            neutral.SetActive(true);
            unsighted_babyPanicked.SetActive(false);
            sighted_babyUnpanicked.SetActive(false);
            sighted_babyPanicked.SetActive(false);

            playerCharacter.OnSighted += UpdateCasonStatus;
            playerCharacter.OnUnsighted += UpdateCasonStatus;

            babyDiscomfort.OnCryingStart += UpdateBabyStatus;
            babyDiscomfort.OnCryingStop += UpdateBabyStatus;
        }

        private void OnDisable()
        {
            playerCharacter.OnSighted -= UpdateCasonStatus;
            playerCharacter.OnUnsighted -= UpdateCasonStatus;

            babyDiscomfort.OnCryingStart -= UpdateBabyStatus;
            babyDiscomfort.OnCryingStop -= UpdateBabyStatus;
        }

        // Update is called once per frame
        void Update()
        {
            //UpdateUI();
        }

        public void UpdateBabyStatus(EBabyStatus babyStatus)
        {
            currentBabyStatus = babyStatus;
            UpdateUI();
        }

        public void UpdateCasonStatus(ECasonStatus casonStatus)
        {
            currentCasonStatus = casonStatus;
            UpdateUI();
        }

        private void UpdateUI()
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

    }
}