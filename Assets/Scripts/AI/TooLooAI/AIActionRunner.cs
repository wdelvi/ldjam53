using System;
using UnityEngine;

namespace TooLoo.AI
{
    public class AIActionRunner : MonoBehaviour
    {
        [SerializeField] protected AIAgent blackboard;

        protected AIAction currentAction;

        protected bool isRunning;
        protected float actionProgress;

        public Action OnFinishedAction;

        public bool IsRunning => isRunning;
        public float ActionProgress => actionProgress;
        public AIAction CurrentAction => currentAction;

        // Start is called before the first frame update
        public virtual void Init()
        {
            actionProgress = 0f;
            isRunning = false;
        }

        protected virtual void OnDisable()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (isRunning)
            {
                currentAction?.UpdateAction(blackboard);
            }
        }

        public void OnReachedActionTarget()
        {
            StartAction();
        }

        public void OnSelectedAction()
        {
            actionProgress = 0f;
            currentAction?.StopAction(blackboard);

            currentAction = AIAction.Get(blackboard.CurrentActionId);
            currentAction.OnSelected(blackboard);
        }

        public void StartAction()
        {
            if (isRunning) return;

            if (currentAction.IsAchievable(blackboard))
            {
                isRunning = true;
                currentAction.StartAction(blackboard);
            }
            else
            {
                isRunning = false;
                currentAction = null;
                OnFinishedAction?.Invoke();
            }
        }

        // Called from within the current action
        public void StopAction()
        {
            isRunning = false;
            actionProgress = 0f;
            currentAction?.StopAction(blackboard);
            currentAction = null;

            OnFinishedAction?.Invoke();
        }

        public void AddActionProgress(float amount)
        {
            actionProgress += amount;
        }

        public void ResetActionProgress()
        {
            actionProgress = 0f;
        }
    }
}