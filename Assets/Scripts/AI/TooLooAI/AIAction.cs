using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public abstract class AIAction : ScriptableObject
    {
        [SerializeField, ReadOnly]
        protected string id = string.Empty;

        [TextArea(1,5)]
        public string description;

        public string Id => id;

        protected static List<AIAction> list = new();

        public static void Load(string folder)
        {
            list.Clear();
            list.AddRange(Resources.LoadAll<AIAction>(folder));
        }

        public static AIAction Get(string id)
        {
            foreach (AIAction action in list)
            {
                if (action.Id == id)
                    return action;
            }
            return null;
        }

        public static T Get<T>() where T : AIAction
        {
            foreach (AIAction action in list)
            {
                if (action is T)
                    return (T)action;
            }
            return null;
        }

        public static List<AIAction> GetAll()
        {
            return list;
        }

        public void GenerateUID()
        {
            string uniqueId = System.Guid.NewGuid().ToString();
            if (id.Equals(uniqueId)) return;

            id = uniqueId;
        }

        public abstract bool RequiresInRange(AIAgent agent);

        public abstract bool IsAchievable(AIAgent agent);

        /// <summary>
        /// Use to trigger any other logic upon being selected
        /// </summary>
        /// <param name="blackboard"></param>
        public virtual void OnSelected(AIAgent agent) { }

        /// <summary>
        /// Logic to run before time-dependent logic runs. Good for checking things
        /// or executing instantaneous logic.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public abstract void StartAction(AIAgent agent);

        /// <summary>
        /// Time-dependent action logic goes here. Ex: gathering over time
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public abstract void UpdateAction(AIAgent agent);

        /// <summary>
        /// Logic to run after time-dependent logic has run. Good for any cleanup
        /// or post-action checks.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        public abstract void StopAction(AIAgent agent);

        protected virtual void FaceTowards(Vector3 target, Transform unit)
        {
            Vector3 facing;
            facing = target - unit.position;
            facing.y = 0f;
            facing.Normalize();

            //Apply Rotation
            Quaternion targ_rot = Quaternion.LookRotation(facing, Vector3.up);
            Quaternion nrot = Quaternion.RotateTowards(unit.rotation, targ_rot, 360f);
            unit.rotation = nrot;
        }
    }
}