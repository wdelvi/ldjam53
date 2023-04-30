using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo.AI;

namespace YATE.AI
{
    public class EnemyAIBrain : AIBrain
    {
        [SerializeField] protected EnemyAIAgent agent;

        public override void Init()
        {
            base.Init();
            (agent.Sensor as EnemyVisualSensor).OnDetectedPlayer += OnDetectedPlayer;
        }

        protected void OnDisable()
        {
            (agent.Sensor as EnemyVisualSensor).OnDetectedPlayer -= OnDetectedPlayer;
        }

        public override void DecideBehaviour()
        {

        }

        // TODO - Implement reaction to player detection
        protected virtual void OnDetectedPlayer(PlayerCharacter player)
        {
            if (agent.PlayerCharacterTarget is null)
            {
                agent.SetTarget(player);
                (agent.MovementAI as EnemyMovementAI).OnAcquireTarget();
            }
        }
    }
}
