using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo.AI;

namespace YATE.AI
{
    public class EnemyAIBrain : AIBrain
    {
        [SerializeField] private EnemyAIAgent agent;

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

        private void OnDetectedPlayer()
        {
            Debug.Log("Detected Player!!");
        }
    }
}