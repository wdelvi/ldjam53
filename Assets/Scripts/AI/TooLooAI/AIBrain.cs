using System;
using UnityEngine;

namespace TooLoo.AI
{
    public abstract class AIBrain : MonoBehaviour
    {
        public Action OnSelectedBehaviour;

        public virtual void Init() { }

        public abstract void DecideBehaviour();
    }
}