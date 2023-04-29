using System;
using UnityEngine;
using TooLoo;

namespace YATE.AI
{
    public class PatrolState : MovementState
    {
        private Vector3 destination;

        public event Action OnAcquireTarget;

        private float waitTimer;
        private float maxIdleTime;
        private float idleTime;

        public PatrolState(EnemyAIAgent agent, float maxIdleTime)
        {
            this.agent = agent;
            this.maxIdleTime = maxIdleTime;
        }

        public override void OnEnter()
        {
            destination = default;
            idleTime = UnityEngine.Random.Range(1f, maxIdleTime);
        }

        public override void OnUpdate()
        {
            if (agent.AgentTarget != null)
            {
                OnAcquireTarget?.Invoke();
                return;
            }

            HandleRotation();

            if (destination == default)
            {
                destination = Utils.GetRandomNavMeshPosition(agent.AnchorPoint.position, 20f);
                agent.Navigator.SetDestination(destination);
                return;
            }

            if (Vector3.Distance(destination, agent.transform.position) <= 1f)
            {
                HandleWaiting();
            }
        }

        private void HandleWaiting()
        {
            if (waitTimer < idleTime)
            {
                waitTimer += Time.deltaTime;
                return;
            }
            else
            {
                waitTimer = 0f;
                idleTime = UnityEngine.Random.Range(1f, maxIdleTime);
                agent.Navigator.ResetPath();
                destination = default;
            }
        }
    }
}