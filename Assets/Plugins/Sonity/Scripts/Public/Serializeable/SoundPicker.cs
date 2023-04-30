// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using Sonity.Internal;
using System;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundPicker"/> is a serializable class for easily selecting multiple <see cref="SoundEvent"/>s and modifiers.
    /// Add a serialized or public <see cref="SoundPicker"/> to a C# script and edit it in the inspector.
    /// <see cref="SoundPicker"/> are multi-object editable.
    /// <code>
    /// <para/> // Example use
    /// <para/> public Sonity.SoundPicker soundPicker;
    /// <para/> private void Start() {
    /// <para/>     soundPicker.Play(transform);
    /// <para/> } 
    /// </code>
    /// </summary>
    [Serializable]
    public class SoundPicker {

        public SoundPickerInternals internals = new SoundPickerInternals();

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='owner'>
        /// The <see cref="Transform"/> where is should play at
        /// </param>
        public void Play(Transform owner) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].Play(owner, null, null, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The <see cref="Transform"/> where is should play at
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play(Transform owner, SoundTag localSoundTag) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].Play(owner, null, null, localSoundTag);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='owner'>
        /// The <see cref="Transform"/> where is should play at
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play(Transform owner, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].Play(owner, soundParameters, null, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The <see cref="Transform"/> where is should play at
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play(Transform owner, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].Play(owner, soundParameters, null, localSoundTag);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        public void PlayAtPosition(Transform owner, Transform position) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The owner {nameof(Transform)} is null.", position);
                    }
                } else {
                    if (internals.isEnabled) {
                        for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                            if (internals.soundPickerPart[i].soundEvent != null) {
                                internals.soundPickerPart[i].PlayAtPosition(position, owner, null, null, null);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Transform owner, Transform position, SoundTag localSoundTag) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The owner {nameof(Transform)} is null.", position);
                    }
                } else {
                    if (internals.isEnabled) {
                        for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                            if (internals.soundPickerPart[i].soundEvent != null) {
                                internals.soundPickerPart[i].PlayAtPosition(owner, position, null, null, localSoundTag);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayAtPosition(Transform owner, Transform position, params SoundParameterInternals[] soundParameters) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The owner {nameof(Transform)} is null.", position);
                    }
                } else {
                    if (internals.isEnabled) {
                        for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                            if (internals.soundPickerPart[i].soundEvent != null) {
                                internals.soundPickerPart[i].PlayAtPosition(owner, position, soundParameters, null, null);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Transform owner, Transform position, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The owner {nameof(Transform)} is null.", position);
                    }
                } else {
                    if (internals.isEnabled) {
                        for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                            if (internals.soundPickerPart[i].soundEvent != null) {
                                internals.soundPickerPart[i].PlayAtPosition(owner, position, soundParameters, null, localSoundTag);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Vector3"/> position
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        public void PlayAtPosition(Transform owner, Vector3 position) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].PlayAtPosition(owner, position, null, null, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Vector3"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Transform owner, Vector3 position,  SoundTag localSoundTag) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].PlayAtPosition(owner, position, null, null, localSoundTag);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Vector3"/> position
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayAtPosition(Transform owner, Vector3 position, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].PlayAtPosition(owner, position, soundParameters, null, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Vector3"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Transform owner, Vector3 position, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].PlayAtPosition(owner, position, soundParameters, null, localSoundTag);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> with the owner <see cref="Transform"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void Stop(Transform owner, bool allowFadeOut = true) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].Stop(owner, allowFadeOut);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> played at the position <see cref="Transform"/>
        /// </summary>
        /// <param name='position'>
        /// The <see cref="Transform"/>
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAtPosition(Transform position, bool allowFadeOut = true) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The {nameof(Transform)} is null.");
                }
            } else {
                if (internals.isEnabled) {
                    for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                        if (internals.soundPickerPart[i].soundEvent != null) {
                            internals.soundPickerPart[i].StopAtPosition(position, allowFadeOut);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <para> If playing it returns <see cref="SoundEventState.Playing"/> </para> 
        /// <para> If not playing, but its delayed it returns <see cref="SoundEventState.Delayed"/> </para> 
        /// <para> If not playing and its not delayed it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// <para> If the <see cref="SoundEvent"/> or <see cref="Transform"/> is null it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// </summary>
        /// <param name="owner"> 
        /// The owner <see cref="Transform"/> 
        /// </param>
        /// <returns> 
        /// Returns <see cref="SoundEventState"/> of the <see cref="SoundEvent"/>s <see cref="SoundEventInstance"/> 
        /// </returns>
        public SoundEventState GetSoundEventState(Transform owner) {
            SoundEventState soundEventState = SoundEventState.NotPlaying;
            bool soundEventStateDelayed = false;
            for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                if (internals.soundPickerPart[i].soundEvent != null) {
                    soundEventState = SoundManager.Instance.GetSoundEventState(internals.soundPickerPart[i].soundEvent, owner);
                    if (soundEventState == SoundEventState.Playing) {
                        return SoundEventState.Playing;
                    } else if (soundEventState == SoundEventState.Delayed) {
                        soundEventStateDelayed = true;
                    }
                }
            }
            // If no SoundEventInstance is playing
            if (soundEventStateDelayed) {
                return SoundEventState.Delayed;
            } else {
                return SoundEventState.NotPlaying;
            }
        }

        /// <summary>
        /// Loads the Audio Data of the <see cref="AudioClip"/>(s) of the <see cref="SoundContainer"/>(s) to RAM
        /// </summary>
        public void LoadAudioData() {
            for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                internals.soundPickerPart[i].LoadAudioData();
            }
        }

        /// <summary>
        /// Unloads the Audio Data of the <see cref="AudioClip"/>(s) of the <see cref="SoundContainer"/>(s) from RAM
        /// </summary>
        public void UnloadAudioData() {
            for (int i = 0; i < internals.soundPickerPart.Length; i++) {
                internals.soundPickerPart[i].UnloadAudioData();
            }
        }
    }
}