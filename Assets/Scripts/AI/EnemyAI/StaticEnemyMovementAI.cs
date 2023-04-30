using System.Collections;
using System.Collections.Generic;
using TooLoo.AI.FSM;
using UnityEngine;

namespace YATE.AI
{
    public class StaticEnemyMovementAI : MovementFSM
    {
        [Header("YATE Enemy Movement AI Override Settings")]
        [SerializeField] private EnemyAIAgent agent;
        [SerializeField] private float maxIdleTime = 5f;
        [SerializeField] private float patrolRotationSpeed = 1f;
        [SerializeField] private float chaseRotationSpeed = 10f;

        private StaticPatrolState patrolState;
        private StaticChaseState chaseState;
        private CombatState combatState;

        public float PatrolRotationSpeed => patrolRotationSpeed;
        public float ChaseRotationSpeed => chaseRotationSpeed;

        public override void Init()
        {
            patrolState = new StaticPatrolState(agent, maxIdleTime, patrolRotationSpeed);
            chaseState = new StaticChaseState(agent, chaseRotationSpeed);
            combatState = new CombatState(agent);

            fsm.DefaultState = patrolState;

            chaseState.OnTargetGone += OnTargetGone;
            chaseState.OnEnteredCombatRange += OnEnteredCombatRange;

            combatState.OnTargetOutsideCombatRange += OnTargetOutsideCombatRange;
            combatState.OnTargetGone += OnTargetGone;
        }

        private void OnDisable()
        {
            fsm.Clear();

            chaseState.OnTargetGone -= OnTargetGone;
            chaseState.OnEnteredCombatRange -= OnEnteredCombatRange;

            combatState.OnTargetOutsideCombatRange -= OnTargetOutsideCombatRange;
            combatState.OnTargetGone -= OnTargetGone;
        }

        public void OnAcquireTarget()
        {
            fsm.TransitionTo(chaseState);
        }

        private void OnEnteredCombatRange()
        {
            fsm.TransitionTo(combatState);
            Debug.Log("Attack!");
        }

        private void OnTargetOutsideCombatRange()
        {
            fsm.TransitionTo(chaseState);
        }

        private void OnTargetGone()
        {
            agent.ClearTarget();
            fsm.TransitionTo(patrolState);
        }
    }
}