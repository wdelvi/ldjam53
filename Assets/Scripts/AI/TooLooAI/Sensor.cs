using System;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public abstract class Sensor : MonoBehaviour
    {
        [Range(5f, 15f)]
        [SerializeField] protected float radius = 15f;

        [Tooltip("Seconds between each scan")]
        [SerializeField] protected float sensingFrequency = 0.5f;
        [SerializeField] protected LayerMask detectionLayerMasks;

        protected bool sensorOn;

        protected readonly List<ICharacter> targets = new();
        protected readonly List<InteractableBase> interactables = new();

        public Action OnSensorScan;

        public IReadOnlyList<ICharacter> Targets => targets;
        public IReadOnlyList<InteractableBase> Interactables => interactables;

        public float Radius => radius;

        public virtual void Init()
        {
            sensorOn = true;
            InvokeRepeating("ScanEnvironment", 0, sensingFrequency);
        }

        public abstract void ScanEnvironment();

        public virtual void TurnOn()
        {
            sensorOn = true;
        }

        public virtual void TurnOff()
        {
            sensorOn = false;
        }
    }
}