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
        private float rotationRange;
        private int rotateCount;

        private Quaternion targetRotation;
        private Vector3 currentLookAt;
        private Queue<Vector3> targetLookAts;

        public StaticPatrolState(
            EnemyAIAgent agent, 
            float maxIdleTime, 
            float rotateSpeed, 
            float rotationRange,
            int rotateCount)
        {
            this.agent = agent;
            this.maxIdleTime = maxIdleTime;
            this.rotateSpeed = rotateSpeed;
            this.rotationRange = rotationRange;
            this.rotateCount = rotateCount;
        }

        public override void OnEnter()
        {
            Vector3[] positions = Utils.GeneratePositionsOnCircleSection(
                agent.transform, 
                agent.FOV.ViewRadius, 
                rotationRange, 
                rotateCount);

            targetLookAts = new Queue<Vector3>(positions);
            currentLookAt = targetLookAts.Dequeue();
            targetRotation = GetTargetRotation();
        }

        public override void OnExit()
        {
            waitTimer = 0;
        }

        public override void OnUpdate()
        {
            if (Quaternion.Angle(agent.transform.rotation, targetRotation) <= 1f)
            {
                HandleWaiting();
            }
            else
            {
                HandleRotation();
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
                targetLookAts.Enqueue(currentLookAt);
                currentLookAt = targetLookAts.Dequeue();
                targetRotation = GetTargetRotation();
            }
        }
        
        private Quaternion GetTargetRotation()
        {
            Vector3 facing = currentLookAt - agent.transform.position;
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