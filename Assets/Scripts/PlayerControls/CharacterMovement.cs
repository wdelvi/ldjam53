using System.Collections;
using System.Collections.Generic;
using TooLoo;
using UnityEngine;

namespace YATE
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField, ReadOnly] private bool isSprinting;
        [SerializeField, ReadOnly] private float finalMoveSpeed;

        public bool IsMoving => characterController.velocity.magnitude > 0.1f;
        public bool IsSprinting => isSprinting;

        // Update is called once per frame
        void Update()
        {
            if (characterController != null)
            {
                HandleMovement();
            }
        }

        private void HandleMovement()
        {
            Vector3 inputDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) inputDir.z = 1f;
            if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
            if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
            if (Input.GetKey(KeyCode.D)) inputDir.x = 1f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
                finalMoveSpeed = moveSpeed * 2f;
            }
            else
            {
                isSprinting = false;
                finalMoveSpeed = moveSpeed;
            }

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            characterController.SimpleMove(moveDir * finalMoveSpeed);
        }
    }
}