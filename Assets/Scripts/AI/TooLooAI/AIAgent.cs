using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TooLoo.AI.FSM;
using System.Linq;

namespace TooLoo.AI
{
    public class AIAgent : MonoBehaviour, ICharacter
    {
        [SerializeField, ReadOnly] protected string id;

        [Header("Debug Option")]
        [SerializeField] protected bool debug = false;

        [Header("Agent Components")]
        [SerializeField] protected NavMeshAgent navigator;
        [SerializeField] protected Sensor sensor;
        [SerializeField] protected MovementFSM movementAI;
        [SerializeField] protected AIActionRunner actionRunner;
        [SerializeField] protected AIBrain aiBrain;

        protected AIAgent agentTarget;
        protected InteractableBase interactableTarget;
        protected Vector3 targetPosition;
        protected float targetInteractRange;

        public NavMeshAgent Navigator => navigator;
        public Sensor Sensor => sensor;
        public MovementFSM MovementAI => movementAI;
        public AIActionRunner ActionRunner => actionRunner;
        public AIBrain AIBrain => aiBrain;
        public AIAgent AgentTarget => agentTarget;
        public InteractableBase InteractableTarget => interactableTarget;

        public float TargetInteractRange => targetInteractRange;
        public string CurrentActionId { get; set; } = string.Empty;
        public string CurrentMoveBehaviorId { get; set; } = string.Empty;

        public Vector3 TargetPosition
        {
            get
            {
                if (agentTarget != null)
                {
                    return agentTarget.transform.position;
                }

                if (interactableTarget != null)
                {
                    return interactableTarget.transform.position;
                }

                if (targetPosition != default)
                {
                    return targetPosition;
                }

                return transform.position;
            }
        }

        protected readonly static List<AIAgent> agents = new();

        public static AIAgent Get(string id)
        {
            return agents.Where(a => a.id == id).FirstOrDefault();
        }

        public static List<AIAgent> GetAll()
        {
            return agents;
        }

        protected virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            id = System.Guid.NewGuid().ToString();

            sensor?.Init();
            aiBrain?.Init();
            movementAI?.Init();
            actionRunner?.Init();

            agents.Add(this);
        }

        public virtual void SetTarget(AIAgent agent)
        {
            agentTarget = agent;
            interactableTarget = null;
            targetPosition = agent.transform.position;
            targetInteractRange = 2f;
        }

        public virtual void SetTarget(AIAgent agent, float interactRange)
        {
            agentTarget = agent;
            interactableTarget = null;
            targetPosition = agent.transform.position;
            targetInteractRange = interactRange;
        }

        public virtual void SetTarget(InteractableBase interactable)
        {
            agentTarget = null;
            interactableTarget = interactable;
            targetPosition = interactable.transform.position;
            targetInteractRange = interactable.InteractRange;
        }

        public virtual void SetTarget(InteractableBase interactable, float interactRange)
        {
            agentTarget = null;
            interactableTarget = interactable;
            targetPosition = interactable.transform.position;
            targetInteractRange = interactRange;
        }

        public virtual void SetTarget(Vector3 position)
        {
            agentTarget = null;
            interactableTarget = null;
            targetPosition = position;
            targetInteractRange = 0.1f;
        }

        public virtual void ClearTarget()
        {
            agentTarget = null;
            interactableTarget = null;
            targetPosition = default;
            targetInteractRange = 0.1f;
        }

        protected virtual void OnDrawGizmos()
        {
            if (debug)
            {
                Gizmos.DrawWireSphere(targetPosition, 0.5f);
            }
        }
    }
}