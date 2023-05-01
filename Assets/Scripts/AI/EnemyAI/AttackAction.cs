using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

namespace YATE.AI
{
    [CreateAssetMenu(fileName = "Attack", menuName = "YATE/AI/Actions/Attack")]
    public class AttackAction : AIAction
    {
        public override bool IsAchievable(AIAgent agent)
        {
            return true;
        }

        public override bool RequiresInRange(AIAgent agent)
        {
            return true;
        }

        public override void StartAction(AIAgent agent)
        {
            FaceTowards(agent.TargetPosition, agent.transform);
            (agent as EnemyAIAgent).AnimationController.Attack(true);
        }

        public override void StopAction(AIAgent agent)
        {
            (agent as EnemyAIAgent).AnimationController.Attack(false);
        }

        public override void UpdateAction(AIAgent agent)
        {
            EnemyAIAgent enemyAgent = agent as EnemyAIAgent;

            if (enemyAgent.PlayerCharacterTarget is null
                || !enemyAgent.PlayerCharacterTarget.IsAlive)
            {
                agent.ActionRunner.StopAction();
                return;
            }
        }
    }
}