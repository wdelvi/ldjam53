using System;
using UnityEngine;

namespace YATE.AI
{
    public class ChaseState : MovementState
    {
        public event Action OnEnteredCombatRange;
        public event Action OnTargetGone;

        private float defaultChaseDistance;
        private float chaseDistance;

        public ChaseState(EnemyAIAgent agent, float defaultChaseDistance)
        {
            this.agent = agent;
            this.defaultChaseDistance = defaultChaseDistance;
            chaseDistance = defaultChaseDistance;
        }

        public void SetChaseDistance(float distance)
        {
            chaseDistance = distance;
        }

        public void ResetChaseDistance()
        {
            chaseDistance = defaultChaseDistance;
        }

        public override void OnEnter()
        {
            agent.Navigator.speed = agent.MovementAI.MaxSpeed;
            agent.Navigator.stoppingDistance = agent.TargetInteractRange;
        }

        public override void OnUpdate()
        {
            HandleRotation();
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (agent.PlayerCharacterTarget is null 
                || !agent.PlayerCharacterTarget.IsAlive
                || Vector3.Distance(agent.PlayerCharacterTarget.transform.position, agent.transform.position) > chaseDistance)
            {
                OnTargetGone?.Invoke();
                return;
            }

            float distanceFromTarget = Vector3.Distance(agent.PlayerCharacterTarget.transform.position, agent.transform.position);

            if (distanceFromTarget > agent.AttackRange)
            {
                agent.Navigator.SetDestination(agent.PlayerCharacterTarget.transform.position);
            }
            else
            {
                OnEnteredCombatRange?.Invoke();
            }
        }
    }
}