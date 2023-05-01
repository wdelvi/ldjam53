using System;
using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;
using YATE.UI;
using TooLoo;

namespace YATE
{
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        [SerializeField] private float startingHealth = 100f;

        public bool IsAlive { get; set; } = true;

        public bool IsSighted { get; set; } = false;

        public event Action<ECasonStatus> OnSighted;
        public event Action<ECasonStatus> OnUnsighted;

        [Tooltip("For Debugging Only")]
        [SerializeField, ReadOnly] private List<AIAgent> enemiesInPursuit = new();

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
    }
}