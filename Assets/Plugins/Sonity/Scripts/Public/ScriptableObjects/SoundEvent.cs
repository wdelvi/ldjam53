// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundEvent"/>s are what you play in Sonity.
    /// They contain <see cref="SoundContainer"/> and options of how the sound should be played.
    /// All <see cref="SoundEvent"/>s are multi-object editable.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "_SE", menuName = "Sonity/SoundEvent", order = 2)]
    public class SoundEvent : ScriptableObject {

        public SoundEventInternals internals = new SoundEventInternals();

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        public void Play(Transform owner) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play(this, owner);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play(Transform owner, SoundTag localSoundTag) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play(this, owner, localSoundTag);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play(Transform owner, params SoundParameterInternals[] soundParameters) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play(this, owner, soundParameters);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play(Transform owner, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play(this, owner, localSoundTag, soundParameters);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        public void PlayAtPosition(Transform owner, Transform position) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner with the Local <see cref="SoundTag"/>
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position, localSoundTag);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position, soundParameters);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner with the Local <see cref="SoundTag"/>
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position, localSoundTag, soundParameters);
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position);
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
        public void PlayAtPosition(Transform owner, Vector3 position, SoundTag localSoundTag) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position, localSoundTag);
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position, soundParameters);
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayAtPosition(this, owner, position, localSoundTag, soundParameters);
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
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Stop(this, owner, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> played at the position <see cref="Transform"/>.
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Transform"/>
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAtPosition(Transform position, bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopAtPosition(this, position, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops all <see cref="SoundEvent"/>s with the owner <see cref="Transform"/>
        /// </summary>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAllAtOwner(Transform owner, bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopAllAtOwner(owner, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> everywhere
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopEverywhere(bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopEverywhere(this, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops the all <see cref="SoundEvent"/>s
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/>s should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        public void StopEverything(bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopEverything(allowFadeOut);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        public void Play2D() {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play2D(this);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with the Local <see cref="SoundTag"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play2D(SoundTag localSoundTag) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play2D(this, localSoundTag);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play2D(params SoundParameterInternals[] soundParameters) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play2D(this, soundParameters);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> with the Local <see cref="SoundTag"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play2D(SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Play2D(this, localSoundTag, soundParameters);
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> at the 2D <see cref="Transform"/>
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void Stop2D(bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.Stop2D(this, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops all <see cref="SoundEvent"/>s at the 2D <see cref="Transform"/>
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAllAt2D(bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopAllAt2D(allowFadeOut);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the music <see cref="Transform"/>
        /// </summary>
        /// <param name="stopAllOtherMusic">
        /// If all other <see cref="SoundEvent"/>s played at the <see cref="SoundManager"/>s music <see cref="Transform"/> should be stopped
        /// </param>
        /// <param name="allowFadeOut">
        /// If the other stopped <see cref="SoundEvent"/> should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        public void PlayMusic(bool stopAllOtherMusic = true, bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayMusic(this, stopAllOtherMusic, allowFadeOut);
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the music <see cref="Transform"/>
        /// </summary>
        /// <param name="stopAllOtherMusic">
        /// If all other <see cref="SoundEvent"/>s played at the <see cref="SoundManager"/>s music <see cref="Transform"/> should be stopped
        /// </param>
        /// <param name="allowFadeOut">
        /// If the other stopped <see cref="SoundEvent"/> should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        /// <param name="soundParameters">
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayMusic(bool stopAllOtherMusic = true, bool allowFadeOut = true, params SoundParameterInternals[] soundParameters) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.PlayMusic(this, stopAllOtherMusic, allowFadeOut, soundParameters);
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> played with <see cref="PlayMusic()"/>
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the other stopped <see cref="SoundEvent"/> should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        public void StopMusic(bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopMusic(this, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops the all <see cref="SoundEvent"/>s played with MusicPlay
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAllMusic(bool allowFadeOut = true) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                SoundManager.Instance.StopAllMusic(allowFadeOut);
            }
        }

        /// <summary>
        /// <para> If playing it returns <see cref="SoundEventState.Playing"/> </para> 
        /// <para> If not playing, but it is delayed it returns <see cref="SoundEventState.Delayed"/> </para> 
        /// <para> If not playing and it is not delayed it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// <para> If the <see cref="SoundEvent"/> or <see cref="Transform"/> is null it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// </summary>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <returns> Returns <see cref="SoundEventState"/> of the <see cref="SoundEvent"/>s <see cref="SoundEventInstance"/> </returns>
        public SoundEventState GetSoundEventState(Transform owner) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                return SoundManager.Instance.GetSoundEventState(this, owner);
            }
            return SoundEventState.NotPlaying;
        }

        /// <summary>
        /// <para> Returns the length (in seconds) of the <see cref="AudioClip"/> in the last played <see cref="AudioSource"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// <para> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </para>
        /// </summary>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <param name="pitchSpeed"> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </param>
        /// <returns> Length in seconds </returns>
        public float GetLastPlayedClipLength(Transform owner, bool pitchSpeed) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                return SoundManager.Instance.GetLastPlayedClipLength(this, owner, pitchSpeed);
            }
            return 0f;
        }

        /// <summary>
        /// <para> Returns the current time (in seconds) of the <see cref="AudioClip"/> in the last played <see cref="AudioSource"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// <para> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </para>
        /// </summary>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <param name="pitchSpeed"> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </param>
        /// <returns> Time in seconds </returns>
        public float GetLastPlayedClipTime(Transform owner, bool pitchSpeed) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                return SoundManager.Instance.GetLastPlayedClipTime(this, owner, pitchSpeed);
            }
            return 0f;
        }

        /// <summary>
        /// <para> Returns the max length (in seconds) of the <see cref="SoundEvent"/> (calculated from the longest audioClip) </para>
        /// <para> Is scaled by the pitch of the <see cref="SoundEvent"/> and <see cref="SoundContainer"/> </para>
        /// <para> Does not take into account random, intensity or parameter pitch </para>
        /// </summary>
        /// <returns> The max length in seconds </returns>
        public float GetMaxLength() {
            return internals.GetMaxLengthWithPitchAndTimeline(false);
        }

        /// <summary>
        /// <para> Returns the time (in seconds) since the <see cref="SoundEvent"/> was played </para>
        /// <para> Calculated using <see cref="Time.realtimeSinceStartup"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// </summary>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <returns> Time in seconds </returns>
        public float GetTimePlayed(Transform owner) {
            if (SoundManager.Instance == null) {
                Debug.LogWarning($"Sonity.{nameof(SoundManager)} is null. Add one to the scene.");
            } else {
                return SoundManager.Instance.GetTimePlayed(this, owner);
            }
            return 0f;
        }

        /// <summary>
        /// Loads the audio data of any <see cref="AudioClip"/>s assigned to the <see cref="SoundContainer"/>s of this <see cref="SoundEvent"/>
        /// </summary>
        public void LoadAudioData() {
            LoadUnloadAudioData(true);
        }

        /// <summary>
        /// Unloads the audio data of any <see cref="AudioClip"/>s assigned to the <see cref="SoundContainer"/>s of this <see cref="SoundEvent"/>
        /// </summary>
        public void UnloadAudioData() {
            LoadUnloadAudioData(false);
        }

        private void LoadUnloadAudioData(bool load) {
            if (internals.soundContainers.Length == 0) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"{nameof(SoundEvent)} " + GetName() + $" has no {nameof(SoundContainer)}.", this);
                }
            } else {
                for (int i = 0; i < internals.soundContainers.Length; i++) {
                    if (internals.soundContainers[i] == null) {
                        if (ShouldDebug.Warnings()) {
                            Debug.LogWarning($"{nameof(SoundEvent)} " + GetName() + $" {nameof(SoundContainer)} " + i + " is null.", this);
                        }
                    } else {
                        internals.soundContainers[i].internals.LoadUnloadAudioData(load, internals.soundContainers[i]);
                    }
                }
            }
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