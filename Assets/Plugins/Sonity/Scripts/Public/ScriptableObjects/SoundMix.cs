// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundMix"/> objects are used for grouped control of e.g. volume for multiple <see cref="SoundEvent"/> at the same time.
    /// They contain a parent <see cref="SoundMix"/> and modifiers for the <see cref="SoundEvent"/>.
    /// All <see cref="SoundMix"/> objects are multi-object editable.
    /// <see cref="SoundMix"/> objects are a higher performance solution of hierarchical volume control compared to AudioMixerGroups.
    /// Example use; set up a “Master_MIX” and a “SFX_MIX” where the Master_MIX is a parent of the SFX_MIX.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "_MIX", menuName = "Sonity/SoundMix", order = 4)]
    public class SoundMix : ScriptableObject {

        public SoundMixInternals internals = new SoundMixInternals();

        /// <summary>
        /// Sets the volume based on decibel
        /// </summary>
        /// <param name="volumeDecibel"> Range NegativeInfinity to 0 </param>
        public void SetVolumeDecibel(float volumeDecibel) {
            Mathf.Clamp(volumeDecibel, Mathf.NegativeInfinity, 0);
            internals.soundEventModifier.volumeDecibel = volumeDecibel;
            internals.soundEventModifier.volumeRatio = VolumeScale.ConvertDecibelToRatio(volumeDecibel);
        }

        /// <summary>
        /// Sets the pitch based on semitones
        /// </summary>
        /// <param name="pitchSemitone"> No range limit </param>
        public void SetPitchSemitone(float pitchSemitone) {
            internals.soundEventModifier.pitchSemitone = pitchSemitone;
            internals.soundEventModifier.pitchRatio = PitchScale.SemitonesToRatio(pitchSemitone);
        }

        // Managed Name
        [NonSerialized]
        private string cachedName;
        public string GetName() {
            if (Application.isPlaying) {
                if (cachedName == null) {
                    cachedName = name;
                }
                return cachedName;
            } else {
                return name;
            }
        }
    }
}