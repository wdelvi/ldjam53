using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sonity;
using TooLoo;

namespace YATE.Audio
{
    public class FootSteps : MonoBehaviour
    {
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private SoundEvent walkingSoundEvent;
        [SerializeField] private SoundEvent sprintingSoundEvent;

        [SerializeField, ReadOnly] private bool isPlayingWalkingFootSteps;
        [SerializeField, ReadOnly] private bool isPlayingSprintingFootSteps;

        public void Init()
        {
            StopWalkingFootSteps();
            StopSprintingFootSteps();
        }

        // Update is called once per frame
        void Update()
        {
            if (characterMovement.IsMoving)
            {
                if (characterMovement.IsSprinting)
                {
                    StopWalkingFootSteps();
                    PlaySprintingFootSteps();
                }
                else
                {
                    StopSprintingFootSteps();
                    PlayWalkingFootSteps();
                }
            }
            else
            {
                StopWalkingFootSteps();
                StopSprintingFootSteps();
            }
        }

        private void PlayWalkingFootSteps()
        {
            if (isPlayingWalkingFootSteps) return;

            walkingSoundEvent?.Play(transform);
            isPlayingWalkingFootSteps = true;
        }

        private void StopWalkingFootSteps()
        {
            walkingSoundEvent?.Stop(transform);
            isPlayingWalkingFootSteps = false;
        }

        private void PlaySprintingFootSteps()
        {
            if (isPlayingSprintingFootSteps) return;

            sprintingSoundEvent?.Play(transform);
            isPlayingSprintingFootSteps = true;
        }

        private void StopSprintingFootSteps()
        {
            sprintingSoundEvent?.Stop(transform);
            isPlayingSprintingFootSteps = false;
        }
    }
}