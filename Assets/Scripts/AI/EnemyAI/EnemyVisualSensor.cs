using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo;
using TooLoo.AI;
using System;

namespace YATE.AI
{
    public class EnemyVisualSensor : Sensor
    {
        [Header("Other Settings")]
        [SerializeField] private float minDetectionAngle = -50f;
        [SerializeField] private float maxDetectionAngle = 50f;

        public Action<PlayerCharacter> OnDetectedPlayer;

        public override void ScanEnvironment()
        {
            if (!sensorOn) return;

            targets.Clear();
            interactables.Clear();

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, detectionLayerMasks);

            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    PlayerCharacter player = colliders[i].transform.GetComponent<PlayerCharacter>();
                    if (player != null)
                    {
                        if (Utils.IsInFOV(transform, player.transform, minDetectionAngle, maxDetectionAngle))
                        {
                            if (Utils.IsInLineOfSight(transform, player.transform, radius))
                            {
                                targets.Add(player);
                                OnDetectedPlayer?.Invoke(player);
                            }
                        }
                    }

                    InteractableBase interactable = colliders[i].transform.GetComponent<InteractableBase>();
                    if (interactable != null)
                    {
                        interactables.Add(interactable);
                    }
                }
            }
        }



        private void OnDrawGizmos()
        {
            float viewAngle = maxDetectionAngle - minDetectionAngle;
            Vector3 directionA = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
            Vector3 directionB = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

            if (targets.Count == 0)
            {
                Debug.DrawRay(transform.position, directionA * radius, Color.green);
                Debug.DrawRay(transform.position, directionB * radius, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, directionA * radius, Color.red);
                Debug.DrawRay(transform.position, directionB * radius, Color.red);
            }
        }
    }
}
