// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundPolyGroup"/> objects are used to create a polyphony limit shared by multiple different <see cref="SoundEvent"/>s.
    /// The priority for voice allocation is calculated by multiplying the priority set in the <see cref="SoundEvent"/> by the volume of the instance.
    /// A perfect use case would be to have a <see cref="SoundPolyGroup"/> for all bullet impacts of all the different materials so they combined dont use too many voices.
    /// If you want simple individual polyphony control, use the polyphony modifier on the <see cref="SoundEvent"/>.
    /// All <see cref="SoundPolyGroup"/> objects are multi-object editable.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "_POL", menuName = "Sonity/SoundPolyGroup", order = 6)]
    public class SoundPolyGroup : ScriptableObject {

        public SoundPolyGroupInternals internals = new SoundPolyGroupInternals();

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