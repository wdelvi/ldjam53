using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YATE
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        private float finalMoveSpeed;

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
                finalMoveSpeed = moveSpeed * 2f;
            }
            else
            {
                finalMoveSpeed = moveSpeed;
            }

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            characterController.Move(moveDir * finalMoveSpeed * Time.deltaTime);
        }
    }
}