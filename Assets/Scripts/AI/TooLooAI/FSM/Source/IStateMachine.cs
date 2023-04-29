using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FSM
{
    public interface IStateMachine
    {
        public void TransitionToDefaultState();

        public void TransitionTo(IState state);
    }
}

