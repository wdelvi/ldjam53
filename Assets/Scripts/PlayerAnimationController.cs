using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YATE
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
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
    }
}