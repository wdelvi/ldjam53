using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FSM
{
    public class MovementFSM : MonoBehaviour
    {
        [Space(10)]
        [Range(1f, 40f)]
        [SerializeField] protected float rotateSpeed = 20f;
        [Range(0f, 5f)]
        [SerializeField] protected float navigatorWalkSpeed = 2f;
        [Range(0f, 20f)]
        [SerializeField] protected float navigatorMaxSpeed = 8f;

        protected readonly StateMachine fsm = new();

        public float RotateSpeed => rotateSpeed;
        public float WalkSpeed => navigatorWalkSpeed;
        public float MaxSpeed => navigatorMaxSpeed;

        public virtual void Init() { }

        protected virtual void Update()
        {
            if (fsm.CurrentState != null)
            {
                fsm.Update();
            }
        }
    }
}