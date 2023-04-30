// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

#if UNITY_EDITOR

namespace Sonity.Internal {

    public static class EditorTextSoundMix {

        public static string soundMixTooltip =
            $"{nameof(SoundMix)} objects are used for grouped control of e.g. volume for multiple {nameof(SoundEvent)} at the same time." + "\n" +
            "\n" +
            $"They contain a parent {nameof(SoundMix)} and modifiers for the {nameof(SoundEvent)}." + "\n" +
            "\n" +
            $"All {nameof(SoundMix)} objects are multi-object editable." + "\n" +
            "\n" +
            $"{nameof(SoundMix)} objects are a higher performance solution of hierarchical volume control compared to AudioMixerGroups." + "\n" +
            "\n" +
            $"Example use; set up a “Master_MIX” and a “SFX_MIX” where the Master_MIX is a parent of the SFX_MIX.";
    }
}
#endif