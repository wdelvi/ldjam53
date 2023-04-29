using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace TooLoo.AI
{
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected float interactRange = 2f;

        [Header("Smart Object Actions")]
        [SerializeField] protected List<AIAction> actions;

        public float InteractRange => interactRange;

        public List<AIAction> Actions => actions;
    }
}