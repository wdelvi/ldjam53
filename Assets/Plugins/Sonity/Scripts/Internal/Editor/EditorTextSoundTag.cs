// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

namespace Sonity.Internal {

    public static class EditorTextSoundTag {

        public static string soundTagTooltip =
        $"{nameof(SoundTag)} objects are passed to modify how a {nameof(SoundEvent)} should be played." + "\n" +
        "\n" +
        $"You can either pass them when playing a {nameof(SoundEvent)} for setting the local {nameof(SoundTag)}." + "\n" +
        "\n" +
        $"Or you can set the global {nameof(SoundTag)} in the {nameof(SoundManager)}." + "\n" +
        "\n" +
        $"This is useful for e.g; weapon reverb zones." + "\n" +
        "\n" +
        $"Because you can set the {nameof(SoundTag)} corresponding to the acoustic space which the listener is in." + "\n" +
        "\n" +
        $"And when you play the {nameof(SoundEvent)}, your gun reflection layers can correspond to the acoustic space.";
    }
}
#endif