using UnityEngine;
using UnityEngine.AI;
using TooLoo.AI.FSM;
using TooLoo.AI;

namespace YATE.AI
{
    public class EnemyMovementAI : MovementFSM
    {
        [SerializeField] private EnemyAIAgent agent;

        [Header("YATE Enemy Movement AI Settings")]
        [SerializeField] private float maxIdleTime = 5f;

        [Header("Patrol Waypoints")]
        [Tooltip("Agent moves to these waypoints in order")]
        [SerializeField] private Transform[] waypoints;

        private PatrolState patrolState;

        public override void Init()
        {
            patrolState = new PatrolState(agent, waypoints, maxIdleTime);
            fsm.DefaultState = patrolState;
        }

        private void OnDisable()
        {
            fsm.Clear();
        }

        private void OnAcquireTarget()
        {
            //fsm.TransitionTo(chaseState);
        }

        private void OnEnteredCombatRange()
        {
            //fsm.TransitionTo(combatStanceState);
        }

        private void OnTargetOutsideCombatRange()
        {
            //fsm.TransitionTo(chaseState);
        }

        private void OnTargetIsNull()
        {
            //fsm.TransitionTo(patrolState);
        }
    }
}