// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;

namespace Sonity.Internal {

    public static class VolumeScale {

        // The lowest volume in decibels before snapping to -infinity
        public static float lowestVolumeDecibel = -60f;

        // The lowest reverb mix in decibels before snapping to -infinity
        public static float lowestReverbMixDecibel = -80f;

        public static float ConvertRatioToDecibel(float volumeRatio) {
            return 6.0206f * Mathf.Log(volumeRatio, 2f);
        }

        public static float ConvertDecibelToRatio(float volumeDecibel) {
            return Mathf.Pow(2f, volumeDecibel / 6.0206f);
        }
    }
}
