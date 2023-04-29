using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace YATE
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockCameraZ : CinemachineExtension
    {
        [Tooltip("Lock the camera's Z position to this value")]
        public float zPosition = 10;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.z = zPosition;
                state.RawPosition = pos;
            }
        }
    }
}