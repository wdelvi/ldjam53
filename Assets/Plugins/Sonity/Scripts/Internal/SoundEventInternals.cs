// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

namespace Sonity.Internal {

    [Serializable]
    public class SoundEventInternals {

        public SoundEventInternalsData data = new SoundEventInternalsData();

        public SoundContainer[] soundContainers = new SoundContainer[1];

        [NonSerialized]
        public bool poolInitialized = false;
        [NonSerialized]
        public int poolIndex = 0;

        [NonSerialized]
        public bool nullChecked = false;
        [NonSerialized]
        public bool timelineSoundContainerSettingNullChecked = false;

        // Used to nullcheck the timeline settings before playing in playmode
        public SoundEventTimelineData GetTimelineSoundContainerSetting(int index) {
            if (!timelineSoundContainerSettingNullChecked) {
                timelineSoundContainerSettingNullChecked = true;
                if (data.timelineSoundContainerData.Length != soundContainers.Length) {
                    Array.Resize(ref data.timelineSoundContainerData, soundContainers.Length);
                    for (int i = 0; i < data.timelineSoundContainerData.Length; i++) {
                        if (data.timelineSoundContainerData[i] == null) {
                            data.timelineSoundContainerData[i] = new SoundEventTimelineData();
                        }
                    }
                }
            }
            return data.timelineSoundContainerData[index];
        }

        [NonSerialized]
        private bool savedMaxChecked = false;
        [NonSerialized]
        private float savedMaxLength = 0f;

        public float GetMaxLengthWithPitchAndTimeline(bool forceUpdate) {
            if (savedMaxChecked && !forceUpdate) {
                return savedMaxLength;
            } else {
                savedMaxChecked = true;
                float maxLength = 0f;
                // The lowest delay
                float timelineLowestDelay = Mathf.Infinity;
                for (int i = 0; i < soundContainers.Length; i++) {
                    if (timelineLowestDelay > GetTimelineSoundContainerSetting(i).delay) {
                        timelineLowestDelay = GetTimelineSoundContainerSetting(i).delay;
                    }
                }
                for (int i = 0; i < soundContainers.Length; i++) {
                    if (soundContainers[i] != null) {
                        float length = 0f;
                        // Avoid divide by zero
                        if (soundContainers[i].internals.data.pitchRatio > 0f) {
                            // Get the longest AudioClip length and apply pitch ratio
                            length = soundContainers[i].internals.GetLongestAudioClipLength() / soundContainers[i].internals.data.pitchRatio;
                        }

                        // If pitch is used
                        if (data.soundEventModifier.pitchUse) {
                            // Avoid divide by zero
                            if (data.soundEventModifier.pitchRatio > 0f) {
                                // Scaling to the SoundEvent settings pitch ratio
                                length /= data.soundEventModifier.pitchRatio;
                            }
                        }

                        // Adding the timeline delay
                        length += GetTimelineSoundContainerSetting(i).delay - timelineLowestDelay;

                        // The longest length
                        if (maxLength < length) {
                            maxLength = length;
                        }
                    }
                }
                return maxLength;
            }
        }

        [NonSerialized]
        private bool savedMinLengthFirstSoundContainerChecked = false;
        [NonSerialized]
        private float savedMinLengthFirstSoundContainerLength = 0f;

        public float GetMinLengthWithoutPitchOnlyFirstSoundContainer(bool forceUpdate) {
            if (savedMinLengthFirstSoundContainerChecked && !forceUpdate) {
                return savedMinLengthFirstSoundContainerLength;
            } else {
                savedMinLengthFirstSoundContainerChecked = true;
                if (soundContainers.Length > 0 && soundContainers[0] != null) {
                    savedMinLengthFirstSoundContainerLength = soundContainers[0].internals.GetShortestAudioClipLength();
                } else {
                    savedMinLengthFirstSoundContainerLength = 0;
                }
                return savedMinLengthFirstSoundContainerLength;
            }
        }
    }

    [Serializable]
    public class SoundEventInternalsData {

        public SoundEventInternalsData() {
            // Modifiers which show at default
            soundEventModifier.volumeUse = true;
            soundEventModifier.polyphonyUse = true;
        }

        [NonSerialized]
        private bool triggerOnPlayIsInfiniteLoop = false;
        [NonSerialized]
        private bool triggerOnPlayIsInfiniteLoopChecked = false;
        [NonSerialized]
        private bool triggerOnStopIsInfiniteLoop = false;
        [NonSerialized]
        private bool triggerOnStopIsInfiniteLoopChecked = false;
        // TriggerOnTail should be allowed to loop for e.g. music
        [NonSerialized]
        private float triggerOnTailMinLength = 0f;
        [NonSerialized]
        private bool triggerOnTailLengthTooShort = false;
        [NonSerialized]
        private bool triggerOnTailLengthTooShortChecked = false;

        public bool CheckTriggerOnPlayIsInfiniteLoop(SoundEvent soundEvent, bool forceUpdate) {
            if (triggerOnPlayIsInfiniteLoopChecked && !forceUpdate) {
                return triggerOnPlayIsInfiniteLoop;
            }
            triggerOnPlayIsInfiniteLoopChecked = true;
            triggerOnPlayIsInfiniteLoop = GetIfInfiniteLoop(soundEvent, out SoundEvent infiniteInstigator, out SoundEvent infinitePrevious, TriggerOnType.TriggerOnPlay);
            if (triggerOnPlayIsInfiniteLoop) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundEvent)} TriggerOnPlay: "
                        + "\"" + infiniteInstigator.GetName() + "\" in \"" + infinitePrevious.GetName() + "\" creates an infinite loop", infiniteInstigator);
                }
            }
            return triggerOnPlayIsInfiniteLoop;
        }

        public bool CheckTriggerOnStopIsInfiniteLoop(SoundEvent soundEvent, bool forceUpdate) {
            if (triggerOnStopIsInfiniteLoopChecked && !forceUpdate) {
                return triggerOnStopIsInfiniteLoop;
            }
            triggerOnStopIsInfiniteLoopChecked = true;
            triggerOnStopIsInfiniteLoop = GetIfInfiniteLoop(soundEvent, out SoundEvent infiniteInstigator, out SoundEvent infinitePrevious, TriggerOnType.TriggerOnStop);
            if (triggerOnStopIsInfiniteLoop) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundEvent)} TriggerOnStop: " 
                        + "\"" + infiniteInstigator.GetName() + "\" in \"" + infinitePrevious.GetName() + "\" creates an infinite loop", infiniteInstigator);
                }
            }
            return triggerOnStopIsInfiniteLoop;
        }

        public bool CheckTriggerOnTailLengthTooShort(SoundEvent soundEvent, bool forceUpdate) {
            if (triggerOnTailLengthTooShortChecked && !forceUpdate) {
                return triggerOnTailLengthTooShort;
            }
            triggerOnTailLengthTooShortChecked = true;
            triggerOnTailMinLength = soundEvent.internals.GetMinLengthWithoutPitchOnlyFirstSoundContainer(forceUpdate);
            if (triggerOnTailMinLength < triggerOnTailLength) {
                triggerOnTailLengthTooShort = true;
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundEvent)} \"" + soundEvent.GetName() + "\" Trigger On Tail: Tail Length is longer than the shortest AudioClip on the first SoundContainer.", soundEvent);
                }
            }
            return triggerOnTailLengthTooShort;
        }

        public bool GetIfInfiniteLoop(SoundEvent soundEvent, out SoundEvent infiniteInstigator, out SoundEvent infinitePrevious, TriggerOnType triggerOnType) {
            infiniteInstigator = null;
            infinitePrevious = null;

            if (soundEvent == null) {
                return false;
            }

            List<SoundEvent> toCheck = new List<SoundEvent>();
            List<SoundEvent> isChecked = new List<SoundEvent>();

            toCheck.Add(soundEvent);

            while (toCheck.Count > 0) {
                SoundEvent soundEventChild = toCheck[0];
                toCheck.RemoveAt(0);
                if (soundEventChild != null) {
                    for (int i = 0; i < isChecked.Count; i++) {
                        if (isChecked[i] == soundEventChild) {
                            infiniteInstigator = soundEventChild;
                            return true;
                        }
                    }
                    if (triggerOnType == TriggerOnType.TriggerOnPlay) {
                        if (soundEventChild.internals.data.triggerOnPlayEnable && soundEventChild.internals.data.triggerOnPlaySoundEvents != null) {
                            for (int i = 0; i < soundEventChild.internals.data.triggerOnPlaySoundEvents.Length; i++) {
                                toCheck.Add(soundEventChild.internals.data.triggerOnPlaySoundEvents[i]);
                                infinitePrevious = soundEventChild;
                            }
                        }
                    } else if (triggerOnType == TriggerOnType.TriggerOnStop) {
                        if (soundEventChild.internals.data.triggerOnStopEnable && soundEventChild.internals.data.triggerOnStopSoundEvents != null) {
                            for (int i = 0; i < soundEventChild.internals.data.triggerOnStopSoundEvents.Length; i++) {
                                toCheck.Add(soundEventChild.internals.data.triggerOnStopSoundEvents[i]);
                                infinitePrevious = soundEventChild;
                            }
                        }
                    } else if (triggerOnType == TriggerOnType.TriggerOnTail) {
                        if (soundEventChild.internals.data.triggerOnTailEnable && soundEventChild.internals.data.triggerOnTailSoundEvents != null) {
                            for (int i = 0; i < soundEventChild.internals.data.triggerOnTailSoundEvents.Length; i++) {
                                toCheck.Add(soundEventChild.internals.data.triggerOnTailSoundEvents[i]);
                                infinitePrevious = soundEventChild;
                            }
                        }
                    }
                    // TriggerOnTail should be enabled to loop
                    isChecked.Add(soundEventChild);
                }
            }
            return false;
        }

#if UNITY_EDITOR
        [NonSerialized]
        public int statisticsNumberOfPlays = 0;
        public AudioMixerGroup previewAudioMixerGroup;
#endif
        public bool expandSoundContainers = true;
        public bool expandTimeline = true;
        public bool expandPreview = false;

        public bool expandSettings = false;
        public bool expandTriggerOnPlay = false;
        public bool expandTriggerOnStop = false;
        public bool expandTriggerOnTail = false;
        public bool expandAllSoundTag = false;

        public SoundEventTimelineData[] timelineSoundContainerData = new SoundEventTimelineData[0];

        public void InitializeTimelineSoundContainerSetting(int arrayLength) {
            if (timelineSoundContainerData.Length != arrayLength) {
                Array.Resize(ref timelineSoundContainerData, arrayLength);
            }
            for (int i = 0; i < timelineSoundContainerData.Length; i++) {
                if (timelineSoundContainerData[i] == null) {
                    timelineSoundContainerData[i] = new SoundEventTimelineData();
                }
            }
        }

#if UNITY_EDITOR
        public UnityEngine.Object[] foundReferences = new UnityEngine.Object[0];
#endif

        public bool disableEnable = false;
#if UNITY_EDITOR
        public bool muteEnable = false;
        public bool soloEnable = false;
#endif
        public SoundEventModifier soundEventModifier = new SoundEventModifier();

        // Settings
        public PolyphonyMode polyphonyMode;
        public AudioMixerGroup audioMixerGroup;
        public SoundMix soundMix;
        public SoundPolyGroup soundPolyGroup;
        public float soundPolyGroupPriority = 1f;
        public float cooldownTime = 0f;
        public float probability = 100f;

        // Intensity
        public bool expandIntensity = false;
        public float intensityAdd = 0f;
        public float intensityMultiplier = 1f;
        public float intensityRolloff = 0f;
        public float intensitySeekTime = 0f;
        public AnimationCurve intensityCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public bool intensityThresholdEnable = false;
        public float intensityThreshold = 0f;

#if UNITY_EDITOR
        public bool intensityRecord = false;
        public int intensityDebugResolution = 100;
        public List<float> intensityDebugValueList;
#endif

        private float GetRolloff(float value, float rolloff) {
            return LogLinExp.Get(value, rolloff);
        }

        public float GetScaledIntensity(float unscaledIntensity) {
            return Mathf.Clamp(intensityCurve.Evaluate(GetRolloff(Mathf.Clamp((unscaledIntensity + intensityAdd) * intensityMultiplier, 0f, 1f), -intensityRolloff)), 0f, 1f);
        }

        public float EditorGetIntensityOnlyCurveAndRolloff(float unscaledIntensity) {
            return Mathf.Clamp(intensityCurve.Evaluate(GetRolloff(Mathf.Clamp(unscaledIntensity, 0f, 1f), -intensityRolloff)), 0f, 1f);
        }

        public float GetUnscaledIntensity(float intensity) {
            // Avoid divide by 0
            if (intensityMultiplier == 0f) {
                return 0f;
            } else {
                return intensity / intensityMultiplier - intensityAdd;
            }
        }

        // TriggerOnPlay
        public bool triggerOnPlayEnable = false;
        public SoundEvent[] triggerOnPlaySoundEvents = new SoundEvent[1];
        public WhichToPlay triggerOnPlayWhichToPlay = WhichToPlay.PlayAll;

        // TriggerOnStop
        public bool triggerOnStopEnable = false;
        public SoundEvent[] triggerOnStopSoundEvents = new SoundEvent[1];
        public WhichToPlay triggerOnStopWhichToPlay = WhichToPlay.PlayAll;

        // TriggerOnTail
        public bool triggerOnTailEnable = false;
        public SoundEvent[] triggerOnTailSoundEvents = new SoundEvent[1];
        public WhichToPlay triggerOnTailWhichToPlay = WhichToPlay.PlayAll;
        public float triggerOnTailLength = 0f;
        public float triggerOnTailBpm = 120f;
        public BeatLength triggerOnTailBeatLength = BeatLength.OneBar;

        // Random SoundEvent for Trigger On
        [NonSerialized]
        private int[] triggerOnPlayRandomLast = new int[0];
        [NonSerialized]
        private int triggerOnPlayRandomPosition;
        [NonSerialized]
        private int[] triggerOnStopRandomLast = new int[0];
        [NonSerialized]
        private int triggerOnStopRandomPosition;
        [NonSerialized]
        private int[] triggerOnTailRandomLast = new int[0];
        [NonSerialized]
        private int triggerOnTailRandomPosition;

        public int GetTriggerOnPlayRandomSoundEvent() {
            return GetTriggerOnRandomSoundEvent(ref triggerOnPlayRandomLast, ref triggerOnPlayRandomPosition, triggerOnPlaySoundEvents.Length);
        }

        public int GetTriggerOnStopRandomSoundEvent() {
            return GetTriggerOnRandomSoundEvent(ref triggerOnStopRandomLast, ref triggerOnStopRandomPosition, triggerOnStopSoundEvents.Length);
        }

        public int GetTriggerOnTailRandomSoundEvent() {
            return GetTriggerOnRandomSoundEvent(ref triggerOnTailRandomLast, ref triggerOnTailRandomPosition, triggerOnTailSoundEvents.Length);
        }

        private int GetTriggerOnRandomSoundEvent(ref int[] randomLast, ref int randomPosition, int soundEventLength) {
            // So that it wont break when changing the length in the editor at runtime
            if (randomLast.Length != soundEventLength / 2) {
                randomLast = new int[soundEventLength / 2];
                randomPosition = 0;
            }

            if (randomLast.Length == 0) {
                return 0;
            }

            int randomSoundEvent;
            // Pseudo random function remembering which clips it last played to avoid repetition
            do {
                randomSoundEvent = UnityEngine.Random.Range(0, soundEventLength);
            } while (RandomLastContains(randomLast, randomSoundEvent));
            randomLast[randomPosition] = randomSoundEvent;

            // Wrap index
            randomPosition++;
            if (randomPosition >= randomLast.Length) {
                randomPosition = 0;
            }

            return randomSoundEvent;
        }

        private bool RandomLastContains(int[] randomClipLast, int randomSoundEvent) {
            for (int i = 0; i < randomClipLast.Length; i++) {
                if (randomClipLast[i] == randomSoundEvent) {
                    return true;
                }
            }
            return false;
        }

        // SoundTag
        public bool soundTagEnable = false;
        public SoundTagMode soundTagMode = SoundTagMode.Local;
        public SoundEventSoundTagGroup[] soundTagGroups = new SoundEventSoundTagGroup[1];
    }
}