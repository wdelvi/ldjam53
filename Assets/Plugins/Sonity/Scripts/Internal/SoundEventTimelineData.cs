// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;

namespace Sonity.Internal {

    [Serializable]
    public class SoundEventTimelineData {

        public float delay = 0f;
        public float volumeRatio = 1f;
        public float volumeDecibel = 0f;

        public float GetVolumeRatio() {
            return volumeRatio;
        }

        public float EditorGetVolumeLinear() {
            return Mathf.Clamp(-(volumeDecibel / VolumeScale.lowestVolumeDecibel) + 1f, 0f, 1f);
        }
    }
}