using System;
using UnityEngine;

namespace YATE.AI
{
    public class CombatState : MovementState
    {
        public Action OnTargetOutsideCombatRange;
        public Action OnTargetGone;

        public CombatState(EnemyAIAgent agent)
        {
            this.agent = agent;
        }

        public override void OnEnter()
        {
            agent.Navigator.stoppingDistance = agent.TargetInteractRange;
        }

        public override void OnUpdate()
        {
            if (agent.PlayerCharacterTarget is null 
                || !agent.PlayerCharacterTarget.IsAlive)
            {
                OnTargetGone?.Invoke();
                return;
            }

            float distanceFromTarget = Vector3.Distance(agent.PlayerCharacterTarget.transform.position, agent.transform.position);

            if (distanceFromTarget > agent.TargetInteractRange)
            {
                OnTargetOutsideCombatRange?.Invoke();
                return;
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
            Quaternion nrot = Quaternion.Slerp(agent.transform.rotation, targ_rot, agent.MovementAI.RotateSpeed * Time.deltaTime);
            agent.transform.rotation = nrot;
        }
    }
}