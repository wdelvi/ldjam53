using Sonity;
using System;
using System.Collections;
using System.Collections.Generic;
using TooLoo;
using TooLoo.AI;
using UnityEngine;
using YATE.AI;
using YATE.UI;

namespace YATE
{
    public class BabyDiscomfort : MonoBehaviour
    {
        private PlayerCharacter playerCharacter;
        private CharacterMovement characterMovement;

        [SerializeField] private float startingAmount = 0f;

        [Tooltip("Passive discomfort reduction per second")]
        [SerializeField] private float reduceRate = 1.0f;

        [Tooltip("Discomfort added per second while sprinting")]
        [SerializeField] private float discomfortOnSprint = 10f;

        [Tooltip("Percentage of damage that gets converted to discomfort")]
        [SerializeField] private float discomfortOnDamage = 0.5f;

        [Header("Discomfort Gradient Settings")]

        [Space(10)]

        [Range(0f, 150f)]
        [SerializeField] private float lowDiscomfortMin = 30f;
        [Range(0f, 150f)]
        [SerializeField] private float lowDiscomfortMax = 60f;
        [Tooltip("Baby sounds to play when discomfort is low")]
        [SerializeField] private SoundEvent lowDiscomfortSound;

        [Range(0f, 150f)]
        [SerializeField] private float mediumDiscomfortMin = 60f;
        [Range(0f, 150f)]
        [SerializeField] private float mediumDiscomfortMax = 100f;
        [Tooltip("Baby sounds to play when discomfort is medium")]
        [SerializeField] private SoundEvent mediumDiscomfortSound;

        [Range(0f, 150f)]
        [SerializeField] private float cryingDiscomfortThreshold = 100f;
        [Tooltip("Baby sounds to play when discomfort is >100")]
        [SerializeField] private SoundEvent crySoundEvent;

        [SerializeField, ReadOnly] private float discomfort;
        [SerializeField, ReadOnly] private bool isCrying;
        [SerializeField, ReadOnly] private bool isLowDiscomfort;
        [SerializeField, ReadOnly] private bool isMediumDiscomfort;

        public event Action<EBabyStatus> OnCryingStart;
        public event Action<EBabyStatus> OnCryingStop;

        public void Init(PlayerCharacter playerCharacter)
        {
            discomfort = startingAmount;
            isLowDiscomfort = false;
            isMediumDiscomfort = false;
            isCrying = false;

            this.playerCharacter = playerCharacter;
            characterMovement = playerCharacter?.GetComponent<CharacterMovement>();

            playerCharacter.OnDie += OnDie;
        }

        private void OnDisable()
        {
            playerCharacter.OnDie -= OnDie;
        }

        // Update is called once per frame
        void Update()
        {
            if (discomfort > 0f)
            {
                discomfort = Mathf.Clamp(discomfort - reduceRate * Time.deltaTime, 0f, 150f);
            }

            if (characterMovement.IsSprinting)
            {
                discomfort = Mathf.Clamp(discomfort + discomfortOnSprint * Time.deltaTime, 0f, 150f);
            }

            if (discomfort < lowDiscomfortMin)
            {
                DeactivateAllSound();
            }

            if (discomfort >= lowDiscomfortMin && discomfort < lowDiscomfortMax)
            {
                ActivateLowDiscomfortSound();
            }

            if (discomfort >= mediumDiscomfortMin && discomfort < mediumDiscomfortMax)
            {
                ActivateMediumDiscomfortSound();
            }

            if (discomfort >= cryingDiscomfortThreshold)
            {
                ActivateCrying();
            }
        }

        private void OnDie()
        {
            discomfort = 1000f;
        }

        private void DeactivateAllSound()
        {
            DeactivateCrying();

            isLowDiscomfort = false;
            isMediumDiscomfort = false;

            lowDiscomfortSound?.Play(transform);
            mediumDiscomfortSound?.Stop(transform);
        }

        private void ActivateLowDiscomfortSound()
        {
            if (isLowDiscomfort) return;

            DeactivateCrying();

            isLowDiscomfort = true;
            isMediumDiscomfort = false;

            lowDiscomfortSound?.Play(transform);
            mediumDiscomfortSound?.Stop(transform);            
        }

        private void ActivateMediumDiscomfortSound()
        {
            if (isMediumDiscomfort) return;

            DeactivateCrying();

            isLowDiscomfort = false;
            isMediumDiscomfort = true;

            lowDiscomfortSound?.Stop(transform);
            mediumDiscomfortSound?.Play(transform);
        }

        public void OnTakeDamage(float damage)
        {
            discomfort = Mathf.Clamp(discomfort + damage * discomfortOnDamage, 0f, 100f);
        }

        private void ActivateCrying()
        {
            if (isCrying) return;

            isLowDiscomfort = false;
            isMediumDiscomfort = false;
            isCrying = true;

            lowDiscomfortSound?.Stop(transform);
            mediumDiscomfortSound?.Stop(transform);
            crySoundEvent?.Play(transform);

            AlertAllEnemies();

            OnCryingStart?.Invoke(EBabyStatus.Panicked);
        }

        private void DeactivateCrying()
        {
            if (!isCrying) return;

            isCrying = false;
            crySoundEvent?.Stop(transform);

            DeAlertAllEnemies();

            OnCryingStop?.Invoke(EBabyStatus.Unpanicked);
        }

        private void AlertAllEnemies()
        {
            List<AIAgent> agents = AIAgent.GetAll();

            if (agents.Count == 0) return;

            foreach (AIAgent agent in agents)
            {
                if (agent as EnemyAIAgent)
                {
                    ((agent as EnemyAIAgent)?.MovementAI as EnemyMovementAI)?.SetChaseDistance(1000f);
                    ((agent as EnemyAIAgent)?.AIBrain as EnemyAIBrain)?.OnDetectedPlayer(GetComponent<PlayerCharacter>());                    
                }
            }
        }

        private void DeAlertAllEnemies()
        {
            List<AIAgent> agents = AIAgent.GetAll();

            if (agents.Count == 0) return;

            foreach (AIAgent agent in agents)
            {
                if (agent as EnemyAIAgent)
                {
                    ((agent as EnemyAIAgent)?.MovementAI as EnemyMovementAI)?.ResetChaseDistance();                    
                }
            }
        }
    }
}