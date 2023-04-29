// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System.Collections.Generic;
using System;

namespace Sonity.Internal {

    [Serializable]
    public class SoundMixInternals {

        public SoundEventModifier soundEventModifier = new SoundEventModifier();
        public SoundMix soundMixParent;

        [NonSerialized]
        private bool isInfiniteLoopChecked = false;
        [NonSerialized]
        private bool isInfiniteLoop = false;

        public bool CheckIsInfiniteLoop(SoundMix soundMix, bool forceUpdate) {
            if (isInfiniteLoopChecked && !forceUpdate) {
                return isInfiniteLoop;
            }
            isInfiniteLoopChecked = true;
            isInfiniteLoop = GetIfInfiniteLoop(soundMix, out SoundMix infiniteInstigator, out SoundMix infinitePrevious);
            if (isInfiniteLoop) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundMix)}: " 
                        + "\"" + infiniteInstigator.GetName() + "\" in \"" + infinitePrevious.GetName() + "\" creates an infinite loop", infiniteInstigator);
                }
            }
            return isInfiniteLoop;
        }

        public bool GetIfInfiniteLoop(SoundMix soundMix, out SoundMix infiniteInstigator, out SoundMix infinitePrevious) {

            infiniteInstigator = null;
            infinitePrevious = null;

            if (soundMix == null) {
                return false;
            }

            List<SoundMix> toCheck = new List<SoundMix>();
            List<SoundMix> isChecked = new List<SoundMix>();

            toCheck.Add(soundMix);

            while (toCheck.Count > 0) {
                SoundMix soundMixChild = toCheck[0];
                toCheck.RemoveAt(0);
                if (soundMixChild != null) {
                    for (int i = 0; i < isChecked.Count; i++) {
                        if (isChecked[i] == soundMixChild) {
                            infiniteInstigator = isChecked[i];
                            return true;
                        }
                    }

                    if (soundMixChild.internals.soundMixParent != null) {
                        toCheck.Add(soundMixChild.internals.soundMixParent);
                        infinitePrevious = soundMixChild;
                    }

                    isChecked.Add(soundMixChild);
                }
            }
            return false;
        }
    }
}