// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR


namespace Sonity.Internal {

    public class EditorTextSoundPicker {

        public static string soundPickerTooltip =
            $"{nameof(SoundPicker)} is a serializable class for easily selecting multiple {nameof(SoundEvent)}s and modifiers." + "\n" +
            "\n" +
            $"Add a serialized or public {nameof(SoundPicker)} to a C# script and edit it in the inspector." + "\n" +
            "\n" +
            $"{nameof(SoundPicker)} are multi-object editable.";

        public static string soundEventLabel = $"{nameof(SoundEvent)}";
        public static string soundEventTooltip = $"The {nameof(SoundEvent)} to play.";
    }
}
#endif