using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YATE.AI;
using YATE;
using TooLoo.AI;
using UnityEditor.Timeline.Actions;

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
            if (agent.PlayerCharacterTarget is null)
            {
                agent.SetTarget(agent.FOV.visibleTargets[0].GetComponent<PlayerCharacter>());
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