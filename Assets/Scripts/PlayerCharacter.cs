using System;
using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;
using YATE.UI;
using TooLoo;
using YATE.Audio;

namespace YATE
{
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        [SerializeField, ReadOnly] private float health;
        [SerializeField] private float startingHealth = 100f;

        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private BabyDiscomfort babyDiscomfort;
        [SerializeField] private FootSteps footSteps;
        [SerializeField] private PlayerAnimationController animationController;

        public bool IsAlive => health > 0f;

        public bool IsSighted { get; set; } = false;

        public event Action<ECasonStatus> OnSighted;
        public event Action<ECasonStatus> OnUnsighted;

        public event Action OnDie;

        [Tooltip("For Debugging Only")]
        [SerializeField, ReadOnly] private List<AIAgent> enemiesInPursuit = new();

        private void Start()
        {
            Init(new Vector3(-35f, 0, 0));
        }

        private void Update()
        {
        }

        public void Init(Vector3 startingPosition)
        {
            health = startingHealth;
            characterMovement.Init(this);
            babyDiscomfort.Init(this);
            footSteps.Init();
            animationController.Init(this);
        }

        public void AddEnemyInPursuit(AIAgent enemy)
        {
            enemiesInPursuit.Add(enemy);
            OnSighted?.Invoke(ECasonStatus.Sighted);
        }

        public void RemoveEnemyInPursuit(AIAgent enemy)
        {
            enemiesInPursuit.Remove(enemy);

            if (enemiesInPursuit.Count == 0)
            {
                OnUnsighted?.Invoke(ECasonStatus.Unsighted);
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            babyDiscomfort.OnTakeDamage(damage);

            if (health < 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}