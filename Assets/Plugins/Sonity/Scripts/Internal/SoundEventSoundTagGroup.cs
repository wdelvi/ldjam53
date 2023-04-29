// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using System;

namespace Sonity.Internal {

    [Serializable]
    public class SoundEventSoundTagGroup {
        public SoundTag soundTag;
        public SoundEventModifier soundEventModifierBase = new SoundEventModifier();
        public SoundEventModifier soundEventModifierSoundTag = new SoundEventModifier();
        public SoundEvent[] soundEvent = new SoundEvent[1];
    }
}