using System.Collections;
using System.Collections.Generic;
using TooLoo;
using UnityEngine;
using YATE.AI;

namespace YATE
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private EnemyAIAgent agent;
        [SerializeField] private Animator animator;

        private string SpeedParam = "MoveSpeed";
        private string IsAttackingParam = "IsAttacking";

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat(SpeedParam, agent.Navigator.velocity.magnitude);
        }

        public void Attack(bool state)
        {
            animator.SetBool(IsAttackingParam, state);
        }

        public void AttackEvent()
        {
            if (Vector3.Distance(agent.transform.position, agent.PlayerCharacterTarget.transform.position) <= agent.TargetInteractRange)
            {
                agent.PlayerCharacterTarget.TakeDamage(agent.AttackDamage);
                //Debug.Log("Attacked!");
            }

        }
    }
}