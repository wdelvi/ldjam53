using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YATE.AI;

namespace YATE
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private EnemyAIAgent agent;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Camera cam;
        private Vector3 cameraDirection;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            HandleFlip();

            cameraDirection = cam.transform.forward;
            cameraDirection.y = 0f;

            transform.rotation = Quaternion.LookRotation(cameraDirection);
        }

        private void HandleFlip()
        {
            Vector3 moveDir = agent.Navigator.velocity;

            if (moveDir.x < 0f)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveDir.x > 0f)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}