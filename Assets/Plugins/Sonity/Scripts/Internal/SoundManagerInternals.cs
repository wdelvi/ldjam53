// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Sonity.Internal {

    [Serializable]
    public class SoundManagerInternals {

        public SoundManagerInternalsSettings settings = new SoundManagerInternalsSettings();
#if UNITY_EDITOR
        public SoundManagerInternalsStatistics statistics = new SoundManagerInternalsStatistics();
#endif

        [NonSerialized]
        public bool isGoingToDelete = false;
        [NonSerialized]
        public bool applicationIsQuitting = false;

        [Serializable]
        public class SoundEventPoolValue {
#if UNITY_EDITOR
            public SoundEvent statisticsSoundEvent;
#endif
            // The int is the transform instance ID
            public Dictionary<int, SoundEventInstance> instanceDictionary = new Dictionary<int, SoundEventInstance>();
            public Stack<SoundEventInstance> unusedInstanceStack = new Stack<SoundEventInstance>();
        }

        [NonSerialized]
        public SoundEventPoolValue[] soundEventPool = new SoundEventPoolValue[0];
        [NonSerialized]
        public SoundManagerVoicePool voicePool = new SoundManagerVoicePool();
        [NonSerialized]
        public SoundManagerVoiceEffectPool voiceEffectPool = new SoundManagerVoiceEffectPool();

        // Chached Objects
        [NonSerialized]
        public Transform cachedTransformSoundManager;
        [NonSerialized]
        public GameObject cachedSoundEventPoolGameObject;
        [NonSerialized]
        public Transform cachedSoundEventPoolTransform;
        [NonSerialized]
        public GameObject cachedMusicGameObject;
        [NonSerialized]
        public Transform cachedTransformMusic;
        [NonSerialized]
        public GameObject cachedGameObject2D;
        [NonSerialized]
        public Transform cachedTransform2D;
        [NonSerialized]
        public AudioListener cachedAudioListener;
        [NonSerialized]
        public Transform cachedAudioListenerTransform;

        public void AwakeCheck() {
            FindAudioListener();
            if (settings.dontDestroyOnLoad) {
                // DontDestroyOnLoad only works for root GameObjects
                SoundManager.Instance.gameObject.transform.parent = null;
                UnityEngine.Object.DontDestroyOnLoad(SoundManager.Instance.gameObject);
            }
            if (cachedSoundEventPoolGameObject == null) {
                cachedSoundEventPoolGameObject = new GameObject($"Sonity{nameof(SoundEvent)}Pool");
                cachedSoundEventPoolTransform = cachedSoundEventPoolGameObject.transform;
                // Preload Voices on Awake
                voicePool.CreateVoice(settings.voicePreload, true);
                if (settings.dontDestroyOnLoad) {
                    UnityEngine.Object.DontDestroyOnLoad(cachedSoundEventPoolGameObject);
                }
            }
            if (cachedMusicGameObject == null) {
                cachedMusicGameObject = new GameObject("SonityMusic");
                cachedTransformMusic = cachedMusicGameObject.transform;
                if (settings.dontDestroyOnLoad) {
                    UnityEngine.Object.DontDestroyOnLoad(cachedMusicGameObject);
                }
            }
            if (cachedGameObject2D == null) {
                cachedGameObject2D = new GameObject("Sonity2D");
                cachedTransform2D = cachedGameObject2D.transform;
                if (settings.dontDestroyOnLoad) {
                    UnityEngine.Object.DontDestroyOnLoad(cachedGameObject2D);
                }
            }
#if UNITY_EDITOR
            // audioMasterMute is an editor function
            if (EditorUtility.audioMasterMute && ShouldDebug.Warnings()) {
                Debug.LogWarning($"Sonity sounds will not be heard because \"Mute Audio\" is enabled in the Unity editor");
            }
#endif
        }

        public void Destroy() {
            isGoingToDelete = true;
            OnDestroyForceStopEverything();
            if (cachedSoundEventPoolGameObject != null) {
                UnityEngine.Object.Destroy(cachedSoundEventPoolGameObject);
            }
            if (cachedMusicGameObject != null) {
                UnityEngine.Object.Destroy(cachedMusicGameObject);
            }
            if (cachedGameObject2D != null) {
                UnityEngine.Object.Destroy(cachedGameObject2D);
            }
        }

        public void FindAudioListener() {
            if (cachedAudioListener == null) {
                cachedAudioListener = UnityEngine.Object.FindObjectOfType<AudioListener>();
                if (cachedAudioListener == null) {
                    Debug.LogWarning(
                        $"Sonity.{nameof(SoundManager)} could find no {nameof(AudioListener)} in the scene. " +
                        $"If you are creating one in runtime, make sure it is created in Awake()."
                        );
                } else {
                    cachedAudioListenerTransform = cachedAudioListener.transform;
                }
            }
        }

        public float GetDistanceToAudioListener(Vector3 position) {
            if (cachedAudioListener == null) {
                // Finds the audiolistener
                SoundManager.Instance.Internals.FindAudioListener();
                if (cachedAudioListener == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning(
                            $"Sonity.{nameof(SoundManager)} could find no {nameof(AudioListener)} in the scene. " +
                            $"If you are creating one in runtime, make sure it is created in Awake()."
                            );
                    }
                    return 0f;
                }
            }
            return Vector3.Distance(position, cachedAudioListenerTransform.position);
        }

        public float GetAngleToAudioListener(Vector3 position) {
            if (cachedAudioListener == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning(
                        $"Sonity.{nameof(SoundManager)} could find no {nameof(AudioListener)} in the scene. " +
                        $"If you are creating one in runtime, make sure it is created in Awake()."
                        );
                }
            } else {
                return AngleAroundAxis.Get(position - cachedAudioListenerTransform.position, cachedAudioListenerTransform.forward, Vector3.up);
            }
            return 0f;
        }

        // Managed update has its own temp variables so nothing else can change them while its running
        [NonSerialized]
        private SoundEventInstance managedUpdateTempInstance;
        [NonSerialized]
        private Dictionary<SoundEvent, SoundEventPoolValue>.Enumerator managedUpdateTempInstanceDictionaryValueEnumerator;
        [NonSerialized]
        private Dictionary<int, SoundEventInstance>.Enumerator managedUpdateTempInstanceEnumerator;

        public void ManagedUpdate() {
#if UNITY_EDITOR
            DebugSoundEventsInSceneView();
#endif
            // Managed Update of the SoundEvent Instances
            for (int i = 0; i < soundEventPool.Length; i++) {
                managedUpdateTempInstanceEnumerator = soundEventPool[i].instanceDictionary.GetEnumerator();
                while (managedUpdateTempInstanceEnumerator.MoveNext()) {
                    managedUpdateTempInstance = managedUpdateTempInstanceEnumerator.Current.Value;
                    managedUpdateTempInstance.ManagedUpdate();
                }
            }

            // Waiting to Play (for TriggerOnTail)
            if (toPlayOnTailAdded) {
                toPlayOnTailAdded = false;
                for (int i = 0; i < toPlayOnTailInstances.Length; i++) {
                    if (toPlayOnTailInstances[i] != null) {
                        if (toPlayOnTailInstances[i].managedUpdateWaitingToPlayOnTail) {
                            toPlayOnTailInstances[i].managedUpdateWaitingToPlayOnTail = false;
                            toPlayOnTailInstances[i].TriggerOnTail();
                        }
                        toPlayOnTailInstances[i] = null;
                    }
                }
            }

            // Waiting to pool
            if (toPoolAdded) {
                toPoolAdded = false;
                for (int i = 0; i < toPoolInstances.Length; i++) {
                    if (toPoolInstances[i] != null) {
                        if (toPoolInstances[i].waitingForPooling) {
                            toPoolInstances[i].waitingForPooling = false;
                            // Move instance to stack
                            soundEventPool[toPoolInstances[i].soundEvent.internals.poolIndex].instanceDictionary.Remove(toPoolInstances[i].ownerTransformInstanceID);
                            soundEventPool[toPoolInstances[i].soundEvent.internals.poolIndex].unusedInstanceStack.Push(toPoolInstances[i]);
                            toPoolInstances[i].gameObject.SetActive(false);
                        }
                        toPoolInstances[i] = null;
                    }
                }
            }

            // Stops voices after a certain time
            for (int i = 0; i < voicePool.voiceIndexStopQueue.Count; i++) {
                if (voicePool.voicePool[voicePool.voiceIndexStopQueue.Peek()].GetState() == VoiceState.Pause) {
                    if (voicePool.voicePool[voicePool.voiceIndexStopQueue.Peek()].stopTime < Time.realtimeSinceStartup) {
                        voicePool.voicePool[voicePool.voiceIndexStopQueue.Peek()].SetState(VoiceState.Stop, true);
                        voicePool.voicePool[voicePool.voiceIndexStopQueue.Peek()].cachedGameObject.SetActive(false);
                        voicePool.voiceIndexStopQueue.Dequeue();
                    } else {
                        break;
                    }
                } else {
                    voicePool.voiceIndexStopQueue.Dequeue();
                }
            }
        }

        [NonSerialized]
        private bool toPlayOnTailAdded = false;
        [NonSerialized]
        private SoundEventInstance[] toPlayOnTailInstances = new SoundEventInstance[0];

        public void AddManagedUpdateToPlayOnTail(SoundEventInstance soundEventInstance) {
            toPlayOnTailAdded = true;
            for (int i = 0; i < toPlayOnTailInstances.Length; i++) {
                // Find empty slot
                if (toPlayOnTailInstances[i] == null) {
                    toPlayOnTailInstances[i] = soundEventInstance;
                    return;
                } else {
                    // Done slot
                    if (!toPlayOnTailInstances[i].managedUpdateWaitingToPlayOnTail) {
                        toPlayOnTailInstances[i] = soundEventInstance;
                        return;
                    }
                }
            }
            // If all used, add new slot
            Array.Resize(ref toPlayOnTailInstances, toPlayOnTailInstances.Length + 1);
            toPlayOnTailInstances[toPlayOnTailInstances.Length - 1] = soundEventInstance;
        }

        [NonSerialized]
        private bool toPoolAdded = false;
        [NonSerialized]
        private SoundEventInstance[] toPoolInstances = new SoundEventInstance[0];

        public void AddManagedUpdateToPool(SoundEventInstance soundEventInstance) {
            toPoolAdded = true;
            for (int i = 0; i < toPoolInstances.Length; i++) {
                // Find empty slot
                if (toPoolInstances[i] == null) {
                    toPoolInstances[i] = soundEventInstance;
                    return;
                } else {
                    // Done slot
                    if (!toPoolInstances[i].waitingForPooling) {
                        toPoolInstances[i] = soundEventInstance;
                        return;
                    }
                }
            }
            // If all used, add new slot
            Array.Resize(ref toPoolInstances, toPoolInstances.Length + 1);
            toPoolInstances[toPoolInstances.Length - 1] = soundEventInstance;
        }

        [NonSerialized]
        private SoundEventInstance tempInstance;
        [NonSerialized]
        private Dictionary<int, SoundEventInstance>.Enumerator tempInstanceEnumerator;

        public void PlaySoundEvent(
            SoundEvent soundEvent, SoundEventPlayType playType, Transform owner, Vector3? positionVector, Transform positionTransform,
            SoundEventModifier soundEventModifierSoundPicker, SoundEventModifier soundEventModifierSoundTag, 
            SoundParameterInternals[] soundParameters, SoundParameterInternals soundParameterDistanceScale, SoundTag localSoundTag) {
            if (!settings.disablePlayingSounds && !applicationIsQuitting) {
                // If polyphony should be limited globally
                if (soundEvent.internals.data.polyphonyMode == PolyphonyMode.LimitedGlobally) {
                    // Play only has the owner as position
                    if (playType == SoundEventPlayType.Play) {
                        positionTransform = owner;
                        playType = SoundEventPlayType.PlayAtTransform;
                    }
                    owner = SoundManager.Instance.Internals.cachedTransformSoundManager;
                }

                if (soundEvent.internals.poolInitialized) {
                    // Checks if the SoundEvent should retrigger itself
                    if (soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                        if (!tempInstance.gameObject.activeSelf) {
                            tempInstance.gameObject.SetActive(true);
                        }
                        tempInstance.Play(playType, owner, positionVector, positionTransform, soundEventModifierSoundPicker, soundEventModifierSoundTag, soundParameters, soundParameterDistanceScale, localSoundTag);
                        return;
                    } else {
                        // Checks if there is a unused instance to use
                        if (soundEventPool[soundEvent.internals.poolIndex].unusedInstanceStack.Count > 0) {
                            tempInstance = soundEventPool[soundEvent.internals.poolIndex].unusedInstanceStack.Pop();
                            soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.Add(owner.GetInstanceID(), tempInstance);
                            if (!tempInstance.gameObject.activeSelf) {
                                tempInstance.gameObject.SetActive(true);
                            }
                            tempInstance.Play(playType, owner, positionVector, positionTransform, soundEventModifierSoundPicker, soundEventModifierSoundTag, soundParameters, soundParameterDistanceScale, localSoundTag);
                            return;
                        }
                        // Create a new instance
                        GameObject tempGameObject = new GameObject();
                        tempGameObject.AddComponent<SoundEventInstance>();
                        tempInstance = tempGameObject.GetComponent<SoundEventInstance>();
                        tempInstance.Initialize(soundEvent);
                        tempInstance.GetTransform().parent = cachedSoundEventPoolTransform;
                        soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.Add(owner.GetInstanceID(), tempInstance);
                        tempInstance.Play(playType, owner, positionVector, positionTransform, soundEventModifierSoundPicker, soundEventModifierSoundTag, soundParameters, soundParameterDistanceScale, localSoundTag);
                        return;
                    }
                } else {
                    if (!soundEvent.internals.nullChecked) {
                        soundEvent.internals.nullChecked = true;
                        for (int i = 0; i < soundEvent.internals.soundContainers.Length; i++) {
                            // Checks if the SoundEvent is null
                            if (soundEvent.internals.soundContainers[i] == null) {
                                soundEvent.internals.nullChecked = false;
                                if (ShouldDebug.Warnings()) {
                                    Debug.LogWarning($"Sonity: \"" + soundEvent.GetName() + $"\" ({nameof(SoundEvent)}) has null {nameof(SoundContainer)}s.", soundEvent);
                                }
                                return;
                            } else {
                                // Checks if the AudioClips are not empty
                                if (soundEvent.internals.soundContainers[i].internals.audioClips.Length == 0) {
                                    soundEvent.internals.nullChecked = false;
                                    if (ShouldDebug.Warnings()) {
                                        Debug.LogWarning($"Sonity: \"" + soundEvent.internals.soundContainers[i].GetName() + $"\" ({nameof(SoundContainer)}) has no {nameof(AudioClip)}s.", soundEvent.internals.soundContainers[i]);
                                    }
                                    return;
                                } else {
                                    for (int ii = 0; ii < soundEvent.internals.soundContainers[i].internals.audioClips.Length; ii++) {
                                        // Checks if the AudioClips are null
                                        if (soundEvent.internals.soundContainers[i].internals.audioClips[ii] == null) {
                                            soundEvent.internals.nullChecked = false;
                                            if (ShouldDebug.Warnings()) {
                                                Debug.LogWarning($"Sonity: \"" + soundEvent.internals.soundContainers[i].GetName() + $"\" ({nameof(SoundContainer)}) has null {nameof(AudioClip)}s.", soundEvent.internals.soundContainers[i]);
                                            }
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Create a new instance
                    GameObject tempGameObject = new GameObject();
                    tempGameObject.AddComponent<SoundEventInstance>();
                    tempInstance = tempGameObject.GetComponent<SoundEventInstance>();
                    tempInstance.Initialize(soundEvent);
                    tempInstance.GetTransform().parent = cachedSoundEventPoolTransform;
                    Array.Resize(ref soundEventPool, soundEventPool.Length + 1);
                    soundEventPool[soundEventPool.Length - 1] = new SoundEventPoolValue();
#if UNITY_EDITOR
                    soundEventPool[soundEventPool.Length - 1].statisticsSoundEvent = soundEvent;
#endif
                    soundEventPool[soundEventPool.Length - 1].instanceDictionary.Add(owner.GetInstanceID(), tempInstance);
                    soundEvent.internals.poolInitialized = true;
                    soundEvent.internals.poolIndex = soundEventPool.Length - 1;
                    tempInstance.Play(playType, owner, positionVector, positionTransform, soundEventModifierSoundPicker, soundEventModifierSoundTag, soundParameters, soundParameterDistanceScale, localSoundTag);
                    return;
                }
            }
        }

        public void Stop(SoundEvent soundEvent, Transform owner, bool allowFadeOut) {
            if (soundEvent.internals.poolInitialized) {
                if (soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                    tempInstance.PoolAllVoices(allowFadeOut, true, false);
                    return;
                }
            }
        }

        public void StopAtPosition(SoundEvent soundEvent, Transform position, bool allowFadeOut) {
            if (soundEvent.internals.poolInitialized) {
                tempInstanceEnumerator = soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.GetEnumerator();
                while (tempInstanceEnumerator.MoveNext()) {
                    tempInstanceEnumerator.Current.Value.PoolVoicesWithPositionTransform(position, allowFadeOut);
                }
            }
        }

        public void StopAllAtOwner(Transform owner, bool allowFadeOut) {
            for (int i = 0; i < soundEventPool.Length; i++) {
                if (soundEventPool[i].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                    tempInstance.PoolAllVoices(allowFadeOut, true, false);
                }
            }
        }

        public void StopEverywhere(SoundEvent soundEvent, bool allowFadeOut) {
            if (soundEvent.internals.poolInitialized) {
                tempInstanceEnumerator = soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.GetEnumerator();
                while (tempInstanceEnumerator.MoveNext()) {
                    tempInstanceEnumerator.Current.Value.PoolAllVoices(allowFadeOut, true, false);
                }
            }
        }

        public void StopEverything(bool allowFadeOut) {
            for (int i = 0; i < soundEventPool.Length; i++) {
                tempInstanceEnumerator = soundEventPool[i].instanceDictionary.GetEnumerator();
                while (tempInstanceEnumerator.MoveNext()) {
                    tempInstanceEnumerator.Current.Value.PoolAllVoices(allowFadeOut, true, false);
                }
            }
        }

        public void OnDestroyForceStopEverything() {
            try {
                for (int i = 0; i < soundEventPool.Length; i++) {
                    tempInstanceEnumerator = soundEventPool[i].instanceDictionary.GetEnumerator();
                    while (tempInstanceEnumerator.MoveNext()) {
                        tempInstanceEnumerator.Current.Value.PoolAllVoices(false, true, true);
                    }
                }
            } catch {

            }
        }

        /// <summary>
        /// <para> If playing it returns <see cref="SoundEventState.Playing"/> </para> 
        /// <para> If not playing, but its delayed it returns <see cref="SoundEventState.Delayed"/> </para> 
        /// <para> If not playing and its not delayed it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// <para> If the <see cref="SoundEvent"/> or <see cref="Transform"/> is null it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the <see cref="SoundEventState"/> from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <returns> Returns <see cref="SoundEventState"/> of the <see cref="SoundEvent"/>s <see cref="SoundEventInstance"/> </returns>
        public SoundEventState GetSoundEventState(SoundEvent soundEvent, Transform owner) {
            if (soundEvent == null || owner == null) {
                return SoundEventState.NotPlaying;
            }
            if (soundEvent.internals.poolInitialized) {
                if (soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                    if (tempInstance != null) {
                        return tempInstance.GetSoundEventState();
                    }
                }
            }
            return SoundEventState.NotPlaying;
        }

        /// <summary>
        /// <para> Returns the length of the <see cref="AudioClip"/> in the last played <see cref="AudioSource"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the length from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <param name="pitchSpeed"> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </param>
        /// <returns> Length in seconds </returns>
        public float GetLastPlayedClipLength(SoundEvent soundEvent, Transform owner, bool pitchSpeed) {
            if (soundEvent == null || owner == null) {
                return 0f;
            }
            if (soundEvent.internals.poolInitialized) {
                if (soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                    if (tempInstance != null) {
                        return tempInstance.GetLastPlayedAudioSourceClipLength(pitchSpeed);
                    }
                }
            }
            return 0f;
        }

        /// <summary>
        /// <para> Returns the current time of the <see cref="AudioClip"/> in the last played <see cref="AudioSource"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the time from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <param name="pitchSpeed"> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </param>
        /// <returns> Time in seconds </returns>
        public float GetLastPlayedClipTime(SoundEvent soundEvent, Transform owner, bool pitchSpeed) {
            if (soundEvent == null || owner == null) {
                return 0f;
            }
            if (soundEvent.internals.poolInitialized) {
                if (soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                    if (tempInstance != null) {
                        return tempInstance.GetLastPlayedAudioSourceTime(pitchSpeed);
                    }
                }
            }
            return 0f;
        }

        /// <summary>
        /// <para> Returns the max length of the <see cref="SoundEvent"/> (calculated from the longest audioClip) </para>
        /// <para> Is scaled by the pitch of the <see cref="SoundEvent"/> and <see cref="SoundContainer"/> </para>
        /// <para> Does not take into account random, intensity or parameter pitch </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the length from </param>
        /// <param name="forceUpdate"> Otherwise it will only calculate once (good for runtime performance) </param>
        /// <returns> The max length in seconds </returns>
        public float GetMaxLength(SoundEvent soundEvent, bool forceUpdate) {
            if (soundEvent == null) {
                return 0f;
            }
            return soundEvent.internals.GetMaxLengthWithPitchAndTimeline(forceUpdate);
        }

        /// <summary>
        /// <para> Returns the time since the <see cref="SoundEvent"/> was played </para>
        /// <para> Calculated using <see cref="Time.realtimeSinceStartup"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the time played from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <returns> Time in seconds </returns>
        public float GetTimePlayed(SoundEvent soundEvent, Transform owner) {
            if (soundEvent == null || owner == null) {
                return 0f;
            }
            if (soundEvent.internals.poolInitialized) {
                if (soundEventPool[soundEvent.internals.poolIndex].instanceDictionary.TryGetValue(owner.GetInstanceID(), out tempInstance)) {
                    if (tempInstance != null) {
                        return tempInstance.GetTimePlayed();
                    }
                }
            }
            return 0f;
        }

#if UNITY_EDITOR
        [NonSerialized]
        private List<SoundEvent> soloSoundEvents = new List<SoundEvent>();

        public bool GetSoloEnabled() {
            for (int i = soloSoundEvents.Count - 1; i >= 0; i--) {
                if (soloSoundEvents[i] != null && soloSoundEvents[i].internals.data.soloEnable) {
                    return true;
                } else {
                    soloSoundEvents.RemoveAt(i);
                }
            }
            return false;
        }

        public bool GetIsInSolo(SoundEvent soundEvent) {
            if (soloSoundEvents.Contains(soundEvent)) {
                return true;
            }
            return false;
        }

        public void AddSolo(SoundEvent soundEvent) {
            if (!soloSoundEvents.Contains(soundEvent)) {
                soloSoundEvents.Add(soundEvent);
            }
        }
#endif

#if UNITY_EDITOR
        public void DebugInGameViewUpdate() {
            if (!settings.debugSoundEventsInGameViewEnabled || voicePool.voicePool == null) {
                return;
            }

            for (int i = 0; i < voicePool.voicePool.Length; i++) {
                if (voicePool.voicePool[i] != null
                    && voicePool.voicePool[i].isAssigned
                    && voicePool.voicePool[i].soundEvent != null
                    && voicePool.voicePool[i].cachedGameObject != null) {
                    if (voicePool.voicePool[i].GetState() == VoiceState.Play) {
                        EditorDrawDebugTextGame.Draw(
                            voicePool.voicePool[i].soundEvent.GetName(),
                            voicePool.voicePool[i].cachedGameObject.transform.position,
                            settings.debugSoundEventsColorStart,
                            settings.debugSoundEventsColorEnd,
                            settings.debugSoundEventsColorOutline,
                            voicePool.voicePool[i].GetTimePlayed(),
                            settings.debugSoundEventsVolumeToAlpha,
                            settings.debugSoundEventsLifetimeToAlpha,
                            settings.debugSoundEventsLifetimeColorLength,
                            voicePool.voicePool[i].GetVolumeRatioWithFade(),
                            settings.debugSoundEventsFontSize
                            );
                    }
                }
            }
        }

        [NonSerialized]
        public bool debugSoundEventsInSceneViewAdded = false;

        public void DebugSoundEventsInSceneView() {
#if UNITY_2019_1_OR_NEWER
            if (settings.debugSoundEventsInSceneViewEnabled) {
                if (!debugSoundEventsInSceneViewAdded) {
                    debugSoundEventsInSceneViewAdded = true;
                    SceneView.duringSceneGui += DebugSoundEventsInSceneViewSceneView;
                }
            } else {
                if (debugSoundEventsInSceneViewAdded) {
                    debugSoundEventsInSceneViewAdded = false;
                    SceneView.duringSceneGui -= DebugSoundEventsInSceneViewSceneView;
                }
            }
#else
            // Code for older because SceneView.duringSceneGui doesnt exist
#endif
        }

        private void DebugSoundEventsInSceneViewSceneView(SceneView sceneview) {
            if (voicePool.voicePool == null) {
                return;
            }
            if (!settings.debugSoundEventsInSceneViewEnabled) {
                return;
            }
            for (int i = 0; i < voicePool.voicePool.Length; i++) {
                if (voicePool.voicePool[i] != null
                    && voicePool.voicePool[i].isAssigned
                    && voicePool.voicePool[i].soundEvent != null
                    && voicePool.voicePool[i].cachedGameObject != null) {
                    if (voicePool.voicePool[i].GetState() == VoiceState.Play) {
                        EditorDrawDebugTextScene.Draw(
                            voicePool.voicePool[i].soundEvent.GetName(),
                            voicePool.voicePool[i].cachedGameObject.transform.position,
                            settings.debugSoundEventsColorStart,
                            settings.debugSoundEventsColorEnd,
                            settings.debugSoundEventsColorOutline,
                            voicePool.voicePool[i].GetTimePlayed(),
                            settings.debugSoundEventsVolumeToAlpha,
                            settings.debugSoundEventsLifetimeToAlpha,
                            settings.debugSoundEventsLifetimeColorLength,
                            voicePool.voicePool[i].GetVolumeRatioWithFade(),
                            settings.debugSoundEventsFontSize
                            );
                    }
                }
            }
        }
#endif

        public string DebugInfoString(SoundEvent soundEvent, Transform transform, Transform position, Transform owner) {
            string tempString = "";
            bool previousNull = true;

            if (soundEvent != null) {
                previousNull = false;
                tempString += " \"" + soundEvent.name + $"\" ({nameof(SoundEvent)})";
            } else {
                previousNull = true;
            }
            if (transform != null) {
                if (!previousNull) {
                    tempString += ",";
                }
                previousNull = false;
                tempString += " \"" + transform.name + $"\" (Transform)";
            } else {
                previousNull = true;
            }
            if (position != null) {
                if (!previousNull) {
                    tempString += ",";
                }
                previousNull = false;
                tempString += " \"" + position.name + $"\" (position Transform)";
            } else {
                previousNull = true;
            }
            if (owner != null) {
                if (!previousNull) {
                    tempString += ",";
                }
                previousNull = false;
                tempString += " \"" + owner.name + $"\" (owner Transform)";
            } else {
                previousNull = true;
            }
            // Add first At
            if (tempString != "") {
                tempString = " Using" + tempString + ".";
            }
            return tempString;
        }
    }
}