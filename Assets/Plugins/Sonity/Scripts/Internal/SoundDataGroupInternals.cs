// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System.Collections.Generic;
using System;

namespace Sonity.Internal {

    [Serializable]
    public class SoundDataGroupInternals {

        public SoundDataGroup[] soundDataGroupChildren = new SoundDataGroup[0];
        public SoundEvent[] soundEvents = new SoundEvent[1];

        [NonSerialized]
        private bool isInfiniteLoopChecked = false;
        [NonSerialized]
        private bool isInfiniteLoop = false;

        public void LoadUnloadAudioData(bool load, bool includeChildren, SoundDataGroup parent) {
            CheckIsInfiniteLoop(parent, false);
            if (!isInfiniteLoop) {
                if (soundEvents != null) {
                    for (int i = 0; i < soundEvents.Length; i++) {
                        if (soundEvents[i] != null) {
                            if (load) {
                                soundEvents[i].LoadAudioData();
                            } else {
                                soundEvents[i].UnloadAudioData();
                            }
                        }
                    }
                }
                if (includeChildren) {
                    for (int i = 0; i < soundDataGroupChildren.Length; i++) {
                        if (soundDataGroupChildren[i] != null) {
                            soundDataGroupChildren[i].internals.LoadUnloadAudioData(load, includeChildren, soundDataGroupChildren[i]);
                        }
                    }
                }
            }
        }

        public bool CheckIsInfiniteLoop(SoundDataGroup soundDataGroup, bool forceUpdate) {
            if (isInfiniteLoopChecked && !forceUpdate) {
                return isInfiniteLoop;
            }
            isInfiniteLoopChecked = true;
            isInfiniteLoop = GetIfInfiniteLoop(soundDataGroup, out SoundDataGroup infiniteInstigator, out SoundDataGroup infinitePrevious);
            if (isInfiniteLoop) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundDataGroup)}: "
                        + "\"" + infiniteInstigator.GetName() + "\" in \"" + infinitePrevious.GetName() + "\" creates an infinite loop", infiniteInstigator);
                }
            }
            return isInfiniteLoop;
        }

        public bool GetIfInfiniteLoop(SoundDataGroup soundDataGroup, out SoundDataGroup infiniteInstigator, out SoundDataGroup infinitePrevious) {

            infiniteInstigator = null;
            infinitePrevious = null;

            if (soundDataGroup == null) {
                return false;
            }

            List<SoundDataGroup> toCheck = new List<SoundDataGroup>();
            List<SoundDataGroup> isChecked = new List<SoundDataGroup>();

            toCheck.Add(soundDataGroup);

            while (toCheck.Count > 0) {
                SoundDataGroup soundDataGroupChild = toCheck[0];
                toCheck.RemoveAt(0);
                if (soundDataGroupChild != null) {
                    for (int i = 0; i < isChecked.Count; i++) {
                        if (isChecked[i] == soundDataGroupChild) {
                            infiniteInstigator = isChecked[i];
                            return true;
                        }
                    }

                    if (soundDataGroupChild.internals.soundDataGroupChildren != null && soundDataGroupChild.internals.soundDataGroupChildren.Length > 0) {
                        toCheck.AddRange(soundDataGroupChild.internals.soundDataGroupChildren);
                        infinitePrevious = soundDataGroupChild;
                    }
                    isChecked.Add(soundDataGroupChild);
                }
            }
            return false;
        }

    }
}