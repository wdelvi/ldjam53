// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

namespace Sonity.Internal {

    public enum EditorSoundContainerCurveType {
        Volume,
        Pitch,
        StereoPan,
        ReverbZoneMix,
        SpatialBlend,
        SpatialSpread,
        Distortion,
        LowpassFrequency,
        LowpassAmount,
        HighpassFrequency,
        HighpassAmount,
    }
}
#endif