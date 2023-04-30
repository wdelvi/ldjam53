using System.Collections;
using System.Collections.Generic;
using TooLoo;
using UnityEngine;

namespace YATE
{
    public class BabyDiscomfort : MonoBehaviour
    {
        [SerializeField] private CharacterMovement characterMovement;

        [SerializeField] private float startingAmount = 0f;

        [Tooltip("Passive discomfort reduction per second")]
        [SerializeField] private float reduceRate = 1.0f;

        [Tooltip("Discomfort added per second while sprinting")]
        [SerializeField] private float discomfortOnSprint = 2.0f;

        [Tooltip("Percentage of damage that gets converted to discomfort")]
        [SerializeField] private float discomfortOnDamage = 0.5f;

        [SerializeField, ReadOnly] private float discomfort;

        // Start is called before the first frame update
        void Start()
        {
            discomfort = startingAmount;
        }

        // Update is called once per frame
        void Update()
        {
            if (discomfort > 0f)
            {
                discomfort = Mathf.Clamp(discomfort - reduceRate * Time.deltaTime, 0f, 100f);
            }

            if (characterMovement.IsSprinting)
            {
                discomfort = Mathf.Clamp(discomfort + discomfortOnSprint * Time.deltaTime, 0f, 100f);
            }
        }

        public void OnTakeDamage(float damage)
        {
            discomfort = Mathf.Clamp(discomfort + damage * discomfortOnDamage, 0f, 100f);
        }
    }
}