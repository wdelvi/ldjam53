using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

namespace YATE.AI
{
    public class EnemyAIAgent : AIAgent
    {
        [Header("YATE Settings")]
        [SerializeField] protected EnemyAnimationController animationController;
        [SerializeField] protected FieldOfView fov;
        [SerializeField] protected float attackRange = 2f;
        [SerializeField] protected float attackDamage = 30f;

        protected PlayerCharacter playerCharacterTarget;

        public float AttackDamage => attackDamage;
        public float AttackRange => attackRange;
        public PlayerCharacter PlayerCharacterTarget => playerCharacterTarget;
        public FieldOfView FOV => fov;
        public EnemyAnimationController AnimationController => animationController;

        public void SetTarget(PlayerCharacter target)
        {
            playerCharacterTarget = target;
            interactableTarget = null;
            targetPosition = target.transform.position;
            targetInteractRange = attackRange;
        }

        public void SetTarget(PlayerCharacter target, float range)
        {
            playerCharacterTarget = target;
            interactableTarget = null;
            targetPosition = target.transform.position;
            targetInteractRange = range;
        }

        public override void ClearTarget()
        {
            base.ClearTarget();
            playerCharacterTarget = null;
        }
    }
}