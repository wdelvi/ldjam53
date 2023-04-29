// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundTag"/> objects are passed to modify how a <see cref="SoundEvent"/> should be played.
    /// You can either pass them when playing a <see cref="SoundEvent"/> for setting the local <see cref="SoundTag"/>.
    /// Or you can set the global <see cref="SoundTag"/> in the <see cref="SoundManager"/>.
    /// This is useful for e.g; weapon reverb zones.
    /// You can set the <see cref="SoundTag"/> corresponding to the acoustic space which the listener is in.
    /// And when you play the <see cref="SoundEvent"/>, your gun reflection layers can correspond to the acoustic space.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "_TAG", menuName = "Sonity/SoundTag", order = 5)]
    public class SoundTag : ScriptableObject {

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