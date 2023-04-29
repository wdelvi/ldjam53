using UnityEngine;
using TooLoo.AI;
using TooLoo.AI.FSM;

namespace YATE.AI
{
    public abstract class MovementState : State
    {
        protected EnemyAIAgent agent;

        public virtual void HandleRotation()
        {
            if (agent.Navigator.hasPath)
            {
                Vector3 facing = agent.Navigator.steeringTarget - agent.transform.position;
                facing.y = 0f;
                facing.Normalize();

                //Apply Rotation
                Quaternion targ_rot = Quaternion.LookRotation(facing, Vector3.up);
                Quaternion nrot = Quaternion.Slerp(agent.transform.rotation, targ_rot, agent.MovementAI.RotateSpeed * Time.deltaTime);
                agent.transform.rotation = nrot;
            }
        }
    }
}