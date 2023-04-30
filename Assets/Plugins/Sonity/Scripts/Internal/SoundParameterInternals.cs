// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using System;

namespace Sonity.Internal {

    [Serializable]
    public class SoundParameterInternals {

        public SoundParameterInternalsData internals = new SoundParameterInternalsData();

        [Serializable]
        public class SoundParameterInternalsData {

            public SoundParameterType type;
            public UpdateMode updateMode = UpdateMode.Once;

            public float valueFloat;
            public int valueInt;
            public bool valueBool;
        }
    }
}