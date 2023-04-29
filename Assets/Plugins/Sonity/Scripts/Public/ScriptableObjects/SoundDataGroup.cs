// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundDataGroup"/> objects are used to easily load and unload the audio data of the  <see cref="SoundEvent"/>s.
    /// All <see cref="SoundDataGroup"/ objects are multi-object editable.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "_DAT", menuName = "Sonity/SoundDataGroup", order = 7)]
    public class SoundDataGroup : ScriptableObject {

        public SoundDataGroupInternals internals = new SoundDataGroupInternals();

        /// <summary>
        /// Loads the audio data for the <see cref="AudioClip"/>s of the assigned <see cref="SoundEvent"/>s.
        /// </summary>
        /// <param name="includeChildren">
        /// If to load all the audio data of all the child <see cref="SoundDataGroup"/>s also.
        /// </param>
        public void LoadAudioData(bool includeChildren) {
            internals.LoadUnloadAudioData(true, includeChildren, this);
        }

        /// <summary>
        /// Unloads the audio data for the <see cref="AudioClip"/>s of the assigned <see cref="SoundEvent"/>s.
        /// </summary>
        /// <param name="includeChildren">
        /// If to unload all the audio data of all the child <see cref="SoundDataGroup"/>s also.
        /// </param>
        public void UnloadAudioData(bool includeChildren) {
            internals.LoadUnloadAudioData(false, includeChildren, this);
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