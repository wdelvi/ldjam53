using System;
using UnityEngine;
using TooLoo;
using System.Collections.Generic;

namespace YATE.AI
{
    public class PatrolState : MovementState
    {
        private Queue<Transform> waypoints;

        public event Action OnAcquireTarget;

        private Transform currentWaypoint;
        private float waitTimer;
        private float maxIdleTime;
        private float idleTime;

        public PatrolState(EnemyAIAgent agent, Transform[] waypoints, float maxIdleTime)
        {
            this.agent = agent;
            this.waypoints = new Queue<Transform>(waypoints);
            this.maxIdleTime = maxIdleTime;
        }

        public override void OnEnter()
        {
            agent.Navigator.speed = agent.MovementAI.WalkSpeed;
            agent.Navigator.stoppingDistance = 0f;
            idleTime = UnityEngine.Random.Range(1f, maxIdleTime);
        }

        public override void OnExit()
        {
            if (currentWaypoint != null)
            {
                waypoints.Enqueue(currentWaypoint);
                currentWaypoint = null;
            }

            waitTimer = 0f;
        }

        public override void OnUpdate()
        {
            //if (agent.AgentTarget != null)
            //{
            //    OnAcquireTarget?.Invoke();
            //    return;
            //}

            if (currentWaypoint is null)
            {
                currentWaypoint = waypoints.Dequeue();
            }

            if (Vector3.Distance(currentWaypoint.position, agent.transform.position) <= 1f)
            {
                HandleWaiting();
            }
            else
            {
                agent.Navigator.SetDestination(currentWaypoint.position);
            }

            HandleRotation();
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
                waypoints.Enqueue(currentWaypoint);
                currentWaypoint = waypoints.Dequeue();
            }
        }
    }
}