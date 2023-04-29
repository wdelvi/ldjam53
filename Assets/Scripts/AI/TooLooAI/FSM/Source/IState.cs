using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FSM
{
    public interface IState
    {
        public void OnEnter();
        public void OnExit();
        public void OnUpdate();
    }
}