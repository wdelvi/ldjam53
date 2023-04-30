using System.Collections;
using System.Collections.Generic;
using TooLoo;
using Unity.VisualScripting;
using UnityEngine;

namespace YATE.AI
{
    public class StaticPatrolState : MovementState
    {
        private float waitTimer;
        private float maxIdleTime;
        private float idleTime;

        private float rotateSpeed;

        private Quaternion targetRotation;

        public StaticPatrolState(EnemyAIAgent agent, float maxIdleTime, float rotateSpeed)
        {
            this.agent = agent;
            this.maxIdleTime = maxIdleTime;
            this.rotateSpeed = rotateSpeed;
        }

        public override void OnEnter()
        {
            targetRotation = GetTargetRotation();
        }

        public override void OnExit()
        {
            waitTimer = 0;
        }

        public override void OnUpdate()
        {
            if (agent.transform.rotation != targetRotation)
            {
                HandleRotation();
            }
            else
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
                idleTime = Random.Range(1f, maxIdleTime);
                targetRotation = GetTargetRotation();
            }
        }
        
        private Quaternion GetTargetRotation()
        {
            Vector3 facing = Utils.GetRandomCirclePosition(agent.transform.position, 1f, agent.Sensor.Radius) - agent.transform.position;
            facing.y = 0f;
            facing.Normalize();

            return Quaternion.LookRotation(facing, Vector3.up);
        }

        public override void HandleRotation()
        {
            Quaternion nrot = Quaternion.Slerp(agent.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            agent.transform.rotation = nrot;
        }
    }
}