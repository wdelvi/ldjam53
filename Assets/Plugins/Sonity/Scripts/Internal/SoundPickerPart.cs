// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using System;


namespace Sonity.Internal {

    [Serializable]
    public class SoundPickerPart {

        public SoundEvent soundEvent;
        public SoundEventModifier soundEventModifier = new SoundEventModifier();
        public bool expandModifier = true;

        public void Play(Transform owner, SoundParameterInternals[] soundParameters, SoundParameterInternals soundParameterDistanceScale, SoundTag localSoundTag) {
            SoundManager.Instance.Internals.PlaySoundEvent(soundEvent, SoundEventPlayType.Play, owner, null, null, soundEventModifier, null, soundParameters, soundParameterDistanceScale, localSoundTag);
        }

        public void PlayAtPosition(Transform owner, Transform position, SoundParameterInternals[] soundParameters, SoundParameterInternals soundParameterDistanceScale, SoundTag localSoundTag) {
            SoundManager.Instance.Internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtTransform, owner, null, position, soundEventModifier, null, soundParameters, soundParameterDistanceScale, localSoundTag);
        }

        public void PlayAtPosition(Transform owner, Vector3 position, SoundParameterInternals[] soundParameters, SoundParameterInternals soundParameterDistanceScale, SoundTag localSoundTag) {
            SoundManager.Instance.Internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtVector, owner, position, null, soundEventModifier, null, soundParameters, soundParameterDistanceScale, localSoundTag);
        }

        public void Stop(Transform owner, bool allowFadeOut) {
            SoundManager.Instance.Stop(soundEvent, owner, allowFadeOut);
        }

        public void StopAtPosition(Transform position, bool allowFadeOut) {
            SoundManager.Instance.StopAtPosition(soundEvent, position, allowFadeOut);
        }

        public void LoadAudioData() {
            if (soundEvent != null) {
                soundEvent.LoadAudioData();
            }
        }

        public void UnloadAudioData() {
            if (soundEvent != null) {
                soundEvent.UnloadAudioData();
            }
        }
    }
}