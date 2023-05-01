using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YATE
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private PlayerCharacter playerCharacter;
        private CharacterController characterController;

        [SerializeField] private Animator animator;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("MoveSpeed", characterController.velocity.magnitude);
        }

        public void Init(PlayerCharacter playerCharacter)
        {
            this.playerCharacter = playerCharacter;
            characterController = playerCharacter.GetComponent<CharacterController>();

            playerCharacter.OnDie += OnDie;
        }

        private void OnDisable()
        {
            playerCharacter.OnDie -= OnDie;
        }

        private void OnDie()
        {
            animator.SetBool("IsAlive", false);
        }
    }
}