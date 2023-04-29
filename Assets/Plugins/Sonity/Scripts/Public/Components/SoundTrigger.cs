// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundTrigger"/> is a component used for easily playing/stopping <see cref="SoundEvent"/>s on callbacks built into Unity like Enable, Disable, OnCollisionEnter etc.
    /// They contain <see cref="SoundEvent"/>s with modifiers and triggers which decide when it should play or stop.
    /// <see cref="SoundTrigger"/>s also have a radius handle, which is visually editable in the scene viewport for easy adjustment of how far <see cref="SoundEvent"/>s should be heard.
    /// All <see cref="SoundTrigger"/> components are multi-object editable.
    /// </summary>
    [AddComponentMenu("Sonity/Sonity - Sound Trigger")]
	public class SoundTrigger : MonoBehaviour {

        public SoundTriggerInternals internals = new SoundTriggerInternals();

        private void Awake() {
            internals.Initialize(gameObject);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position
        /// </summary>
        public void Play() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].Play(internals.cachedTransform, null, internals.soundParameterDistanceScale, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play(SoundTag localSoundTag) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].Play(internals.cachedTransform, null, internals.soundParameterDistanceScale, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play(params SoundParameterInternals[] soundParameters) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].Play(internals.cachedTransform, soundParameters, internals.soundParameterDistanceScale, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play(SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].Play(internals.cachedTransform, soundParameters, internals.soundParameterDistanceScale, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        public void PlayAtPosition(Transform position) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", internals.cachedTransform);
                }
            } else {
                for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                    if (internals.soundTriggerPart[i].soundEvent != null) {
                        internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, null, internals.soundParameterDistanceScale, null);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Transform position, SoundTag localSoundTag) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", internals.cachedTransform);
                }
            } else {
                for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                    if (internals.soundTriggerPart[i].soundEvent != null) {
                        internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, null, internals.soundParameterDistanceScale, localSoundTag);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayAtPosition(Transform position, params SoundParameterInternals[] soundParameters) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", internals.cachedTransform);
                }
            } else {
                for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                    if (internals.soundTriggerPart[i].soundEvent != null) {
                        internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, soundParameters, internals.soundParameterDistanceScale, null);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Transform position, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundPicker)}: The position {nameof(Transform)} is null.", internals.cachedTransform);
                }
            } else {
                for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                    if (internals.soundTriggerPart[i].soundEvent != null) {
                        internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, soundParameters, internals.soundParameterDistanceScale, localSoundTag);
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
        public void PlayAtPosition(Vector3 position) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, null, internals.soundParameterDistanceScale, null);
                }
            }
        }
        
        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Vector3"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Vector3 position, SoundTag localSoundTag) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, null, internals.soundParameterDistanceScale, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Vector3"/> position
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayAtPosition(Vector3 position, params SoundParameterInternals[] soundParameters) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, soundParameters, internals.soundParameterDistanceScale, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Vector3"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(Vector3 position, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].PlayAtPosition(internals.cachedTransform, position, soundParameters, internals.soundParameterDistanceScale, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> with the owner <see cref="Transform"/>
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void Stop(bool allowFadeOut = true) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].Stop(internals.cachedTransform, allowFadeOut);
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
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    internals.soundTriggerPart[i].StopAtPosition(position, allowFadeOut);
                }
            }
        }

        /// <summary>
        /// <para> If playing it returns <see cref="SoundEventState.Playing"/> </para> 
        /// <para> If not playing, but its delayed it returns <see cref="SoundEventState.Delayed"/> </para> 
        /// <para> If not playing and its not delayed it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// <para> If the <see cref="SoundEvent"/> is null it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// </summary>
        /// <returns>
        /// Returns <see cref="SoundEventState"/> of the <see cref="SoundEvent"/>s <see cref="SoundEventInstance"/> 
        /// </returns>
        public SoundEventState GetSoundEventState() {
            SoundEventState soundEventState = SoundEventState.NotPlaying;
            bool soundEventStateDelayed = false;
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundEvent != null) {
                    soundEventState = SoundManager.Instance.GetSoundEventState(internals.soundTriggerPart[i].soundEvent, internals.cachedTransform);
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
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                internals.soundTriggerPart[i].LoadAudioData();
            }
        }

        /// <summary>
        /// Unloads the Audio Data of the <see cref="AudioClip"/>(s) of the <see cref="SoundContainer"/>(s) from RAM
        /// </summary>
        public void UnloadAudioData() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                internals.soundTriggerPart[i].UnloadAudioData();
            }
        }

        // Basic

        // OnEnable
        private void OnEnable() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onEnableUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnBasic, i, internals.soundTriggerPart[i].soundTriggerTodo.onEnableAction);
                }
            }
        }

        // OnDisable
        private void OnDisable() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onDisableUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnBasic, i, internals.soundTriggerPart[i].soundTriggerTodo.onDisableAction);
                }
            }
        }

        // OnStart
        private void Start() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onStartUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnBasic, i, internals.soundTriggerPart[i].soundTriggerTodo.onStartAction);
                }
            }
        }

        // OnDestroy
        private void OnDestroy() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onDestroyUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnBasic, i, internals.soundTriggerPart[i].soundTriggerTodo.onDestroyAction);
                }
            }
        }

        // Trigger

        // OnTriggerEnter
        private void OnTriggerEnter(Collider other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onTriggerEnterUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnTrigger, i, internals.soundTriggerPart[i].soundTriggerTodo.onTriggerEnterAction, true, other.tag);
                }
            }
        }

        // OnTriggerExit
        private void OnTriggerExit(Collider other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onTriggerExitUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnTrigger, i, internals.soundTriggerPart[i].soundTriggerTodo.onTriggerExitAction, true, other.tag);
                }
            }
        }

        // OnTriggerEnter2D
        private void OnTriggerEnter2D(Collider2D other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onTriggerEnter2DUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnTrigger, i, internals.soundTriggerPart[i].soundTriggerTodo.onTriggerEnter2DAction, true, other.tag);
                }
            }
        }

        // OnTriggerExit2D
        private void OnTriggerExit2D(Collider2D other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onTriggerExit2DUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnTrigger, i, internals.soundTriggerPart[i].soundTriggerTodo.onTriggerExit2DAction, true, other.tag);
                }
            }
        }

        // Collision

        // OnCollisionEnter
        private void OnCollisionEnter(Collision other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onCollisionEnterUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnCollision, i, internals.soundTriggerPart[i].soundTriggerTodo.onCollisionEnterAction, true, other.transform.tag, other, null);
                }
            }
        }

        // OnCollisionExit
        private void OnCollisionExit(Collision other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onCollisionExitUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnCollision, i, internals.soundTriggerPart[i].soundTriggerTodo.onCollisionExitAction, true, other.transform.tag, other, null);
                }
            }
        }

        // OnCollisionEnter2D
        private void OnCollisionEnter2D(Collision2D other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onCollisionEnter2DUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnCollision, i, internals.soundTriggerPart[i].soundTriggerTodo.onCollisionEnter2DAction, true, other.transform.tag, null, other);
                }
            }
        }

        // OnCollisionExit2D
        private void OnCollisionExit2D(Collision2D other) {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onCollisionExit2DUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnCollision, i, internals.soundTriggerPart[i].soundTriggerTodo.onCollisionExit2DAction, true, other.transform.tag, null, other);
                }
            }
        }

        // Mouse

        // OnMouseEnter
        private void OnMouseEnter() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onMouseEnterUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnMouse, i, internals.soundTriggerPart[i].soundTriggerTodo.onMouseEnterAction);
                }
            }
        }

        // OnMouseExit
        private void OnMouseExit() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onMouseExitUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnMouse, i, internals.soundTriggerPart[i].soundTriggerTodo.onMouseExitAction);
                }
            }
        }

        // OnMouseDown
        private void OnMouseDown() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onMouseDownUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnMouse, i, internals.soundTriggerPart[i].soundTriggerTodo.onMouseDownAction);
                }
            }
        }

        // OnMouseUp
        private void OnMouseUp() {
            for (int i = 0; i < internals.soundTriggerPart.Length; i++) {
                if (internals.soundTriggerPart[i].soundTriggerTodo.onMouseUpUse) {
                    internals.DoTriggerAction(SoundTriggerOnType.OnMouse, i, internals.soundTriggerPart[i].soundTriggerTodo.onMouseUpAction);
                }
            }
        }
    }
}