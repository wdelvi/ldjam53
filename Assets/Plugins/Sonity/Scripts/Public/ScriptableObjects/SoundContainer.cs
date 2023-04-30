// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundContainer"/>s are the building blocks of Sonity.
    /// They contain <see cref="AudioClip"/>s and options of how the sound should be played.
    /// All <see cref="SoundContainer"/>s are multi-object editable.
    /// </summary>
    [CreateAssetMenu(fileName = "_SC", menuName = "Sonity/SoundContainer", order = 1)]
    public class SoundContainer : ScriptableObject {

        public SoundContainerInternals internals = new SoundContainerInternals();

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