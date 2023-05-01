using UnityEngine;
using UnityEngine.AI;
using TooLoo.AI.FSM;
using TooLoo.AI;

namespace YATE.AI
{
    public class EnemyMovementAI : MovementFSM
    {
        [Header("YATE Enemy Movement AI Settings")]
        [SerializeField] private EnemyAIAgent agent;
        [SerializeField] private float maxIdleTime = 5f;

        [Header("Patrol Waypoints")]
        [Tooltip("Agent moves to these waypoints in order")]
        [SerializeField] private Transform[] waypoints;

        private PatrolState patrolState;
        private ChaseState chaseState;
        private CombatState combatState;

        public override void Init()
        {
            patrolState = new PatrolState(agent, waypoints, maxIdleTime);
            chaseState = new ChaseState(agent);
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