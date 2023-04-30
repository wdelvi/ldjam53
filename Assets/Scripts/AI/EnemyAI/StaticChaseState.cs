using System;
using UnityEngine;

namespace YATE.AI
{
    public class StaticChaseState : MovementState
    {
        public event Action OnEnteredCombatRange;
        public event Action OnTargetGone;

        private float rotateSpeed;

        public StaticChaseState(EnemyAIAgent agent, float rotateSpeed)
        {
            this.agent = agent;
            this.rotateSpeed = rotateSpeed;
        }

        public override void OnEnter()
        {
            
        }

        public override void OnUpdate()
        {
            if (agent.PlayerCharacterTarget is null
                || !agent.PlayerCharacterTarget.IsAlive
                || Vector3.Distance(agent.PlayerCharacterTarget.transform.position, agent.transform.position) > agent.FOV.ViewRadius)
            {
                OnTargetGone?.Invoke();
                return;
            }

            float distanceFromTarget = Vector3.Distance(agent.PlayerCharacterTarget.transform.position, agent.transform.position);

            if (distanceFromTarget <= agent.AttackRange)
            {
                OnEnteredCombatRange?.Invoke();
            }

            HandleRotation();
        }

        public override void HandleRotation()
        {
            Vector3 facing = agent.PlayerCharacterTarget.transform.position - agent.transform.position;
            facing.y = 0f;
            facing.Normalize();

            //Apply Rotation
            Quaternion targ_rot = Quaternion.LookRotation(facing, Vector3.up);
            Quaternion nrot = Quaternion.Slerp(agent.transform.rotation, targ_rot, rotateSpeed * Time.deltaTime);
            agent.transform.rotation = nrot;
        }
    }
}