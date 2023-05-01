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
        [SerializeField] private CharacterMovement characterMovement;

        [SerializeField] private float startingAmount = 0f;

        [Tooltip("Passive discomfort reduction per second")]
        [SerializeField] private float reduceRate = 1.0f;

        [Tooltip("Discomfort added per second while sprinting")]
        [SerializeField] private float discomfortOnSprint = 10f;

        [Tooltip("Percentage of damage that gets converted to discomfort")]
        [SerializeField] private float discomfortOnDamage = 0.5f;

        [Tooltip("SoundEvent to start playing. Make sure SoundEvent has trigger on tail set up to loop through various crying sequences.")]
        [SerializeField] private SoundEvent crySoundEvent;

        [SerializeField, ReadOnly] private float discomfort;
        [SerializeField, ReadOnly] private bool isCrying;

        public event Action<EBabyStatus> OnCryingStart;
        public event Action<EBabyStatus> OnCryingStop;

        // Start is called before the first frame update
        void Start()
        {
            discomfort = startingAmount;
            isCrying = false;
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

            if (discomfort >= 100f)
            {
                ActivateCrying();
            }

            if (discomfort < 100f)
            {
                DeactivateCrying();
            }
        }

        public void OnTakeDamage(float damage)
        {
            discomfort = Mathf.Clamp(discomfort + damage * discomfortOnDamage, 0f, 100f);
        }

        private void ActivateCrying()
        {
            if (isCrying) return;

            isCrying = true;
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
            foreach (AIAgent agent in agents)
            {
                if (agent as EnemyAIAgent)
                {
                    ((agent as EnemyAIAgent).MovementAI as EnemyMovementAI).SetChaseDistance(1000f);
                    ((agent as EnemyAIAgent).AIBrain as EnemyAIBrain).OnDetectedPlayer(GetComponent<PlayerCharacter>());                    
                }
            }
        }

        private void DeAlertAllEnemies()
        {
            List<AIAgent> agents = AIAgent.GetAll();
            foreach (AIAgent agent in agents)
            {
                if (agent as EnemyAIAgent)
                {
                    ((agent as EnemyAIAgent).MovementAI as EnemyMovementAI).ResetChaseDistance();                    
                }
            }
        }
    }
}