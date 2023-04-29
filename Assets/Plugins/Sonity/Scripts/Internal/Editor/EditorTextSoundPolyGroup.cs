// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

namespace Sonity.Internal {

    public class EditorTextSoundPolyGroup {

        public static string soundPolyGroupTooltip =
            $"{nameof(SoundPolyGroup)} objects are used to create a polyphony limit shared by multiple different {nameof(SoundEvent)}s." + "\n" +
            "\n" +
            $"The priority for voice allocation is calculated by multiplying the priority set in the {nameof(SoundEvent)} by the volume of the instance." + "\n" +
            "\n" +
            $"A perfect use case would be to have a {nameof(SoundPolyGroup)} for all bullet impacts of all the different materials so that when combined, they don’t use too many voices." + "\n" +
            "\n" +
            $"If you want simple individual polyphony control, use the polyphony modifier on the {nameof(SoundEvent)}." + "\n" +
            "\n" +
            $"All {nameof(SoundPolyGroup)} objects are multi-object editable.";

        public static string polyphonyLimitLabel = "Polyphony Limit";
        public static string polyphonyLimitTooltip = $"The maximum number of {nameof(SoundEvent)}s which can be played at the same time in this {nameof(SoundPolyGroup)}.";
    }
}
#endif