// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

namespace Sonity.Internal {

    public class EditorTextSoundDataGroup {

        public static string soundDataGroupTooltip =
            $"{nameof(SoundDataGroup)} objects are used to easily load and unload the audio data of the {nameof(SoundEvent)}s." + "\n" +
            "\n" +
            $"All {nameof(SoundDataGroup)} objects are multi-object editable.";

        public static string childSoundDataGroupsLabel = $"Child {nameof(SoundDataGroup)}s";
        public static string childSoundDataGroupsTooltip = $"Nesting {nameof(SoundDataGroup)}s makes it easy to load/unload all audio data or just parts of it.";

        public static string soundEventsLabel = $"{nameof(SoundEvent)}s";
        public static string soundEventsTooltip = $"The {nameof(SoundEvent)} whoms audio data will be loaded or unloaded.";
    }
}
#endif