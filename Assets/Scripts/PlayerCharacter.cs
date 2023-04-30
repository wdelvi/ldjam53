using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

namespace YATE
{
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        [SerializeField] private float startingHealth = 100f;

        public bool IsAlive { get; set; } = true;
    }
}