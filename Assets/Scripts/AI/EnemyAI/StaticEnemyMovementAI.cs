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

        [Tooltip("Enemy can idle up to this amount of time in seconds")]
        [SerializeField] private float maxIdleTime = 5f;
        
        [Tooltip("How fast the enemy will rotate while it searches")]
        [SerializeField] private float patrolRotationSpeed = 1f;
        
        [Tooltip("How fast the enemy will rotate when it has detected the player")]
        [SerializeField] private float chaseRotationSpeed = 10f;

        [Tooltip("The rotation range the enemy will search in relative to its starting rotation")]
        [Range(0f, 360f)]
        [SerializeField] private float rotationRange = 180f;

        [Tooltip("How many positions the enemy rotate to within rotation range")]
        [SerializeField] private int rotateCount = 5;


        private StaticPatrolState patrolState;
        private StaticChaseState chaseState;
        private CombatState combatState;

        public float PatrolRotationSpeed => patrolRotationSpeed;
        public float ChaseRotationSpeed => chaseRotationSpeed;
        public float RotationRange => rotationRange;
        public int RotateCount => rotateCount;

        public override void Init()
        {
            patrolState = new StaticPatrolState(agent, maxIdleTime, patrolRotationSpeed, rotationRange, rotateCount);
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
            agent.ActionRunner.OnReachedActionTarget();
        }

        private void OnTargetOutsideCombatRange()
        {
            fsm.TransitionTo(chaseState);
            agent.ActionRunner.StopAction();
        }

        private void OnTargetGone()
        {
            agent.ClearTarget();
            fsm.TransitionTo(patrolState);
        }
    }
}