using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YATE.AI;
using YATE;
using TooLoo.AI;

namespace YATE.AI
{
    public class StaticEnemyAIBrain : AIBrain
    {
        [SerializeField] protected EnemyAIAgent agent;
        [SerializeField] protected AIAction attackAction;

        public override void Init()
        {
            base.Init();
            agent.FOV.OnDetectedTarget += OnDetectedPlayer;
        }

        protected void OnDisable()
        {
            agent.FOV.OnDetectedTarget -= OnDetectedPlayer;
        }

        public override void DecideBehaviour()
        {

        }

        // TODO - Implement reaction to player detection
        protected virtual void OnDetectedPlayer()
        {
            OnDetectedPlayer(agent.FOV.visibleTargets[0].GetComponent<PlayerCharacter>());
        }

        public virtual void OnDetectedPlayer(PlayerCharacter playerCharacter)
        {
            if (agent.PlayerCharacterTarget is null)
            {
                agent.SetTarget(playerCharacter);
                agent.PlayerCharacterTarget.AddEnemyInPursuit(agent);
                (agent.MovementAI as StaticEnemyMovementAI).OnAcquireTarget();
            }

            if (agent.ActionRunner.CurrentAction?.Id != attackAction.Id)
            {
                agent.CurrentActionId = attackAction.Id;
                agent.ActionRunner.OnSelectedAction();
            }
        }
    }
}