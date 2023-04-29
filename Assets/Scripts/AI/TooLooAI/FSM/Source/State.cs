using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FSM
{
    public abstract class State : IState
    {
        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public abstract void OnUpdate();
    }
}