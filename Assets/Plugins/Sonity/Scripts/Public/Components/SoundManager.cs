// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// The <see cref="SoundManager"/> is the master object which is used to play sounds and manage global settings.
    /// An instance of this object is required in the scene in order to play <see cref="SoundEvent"/>s.
    /// You can add the pre-made prefab called “SoundManager” found in “Assets\Plugins\Sonity\Prefabs” to your scene.
    /// Or you can add the “Sonity - Sound Manager” component to an empty <see cref="GameObject"/> in the scene, it works just as well.
    /// </summary>
    // ExecuteInEditMode so that stuff can be checked in the editor, e.g. SoundContainer & SoundTrigger distanceScale, guiWarnings, GetIsInSolo
    [ExecuteInEditMode]
    [AddComponentMenu("Sonity/Sonity - Sound Manager")]
    public class SoundManager : MonoBehaviour {

        /// <summary>
        /// The static instance of the <see cref="SoundManager"/>
        /// </summary>
        public static SoundManager Instance { get; private set; }

        [SerializeField]
        private SoundManagerInternals internals = new SoundManagerInternals();

        /// <summary>
        /// The internal data of the <see cref="SoundManager"/>
        /// </summary>
        public SoundManagerInternals Internals { get { return internals; } private set { internals = value; } }

        private void InstanceCheck() {
            if (Instance == null) {
                Instance = this;
                if (Application.isPlaying) {
                    internals.cachedTransformSoundManager = GetComponent<Transform>();
                }
            } else if (Instance != this) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"There can only be one Sonity.{nameof(SoundManager)} instance per scene.", this);
                }
                // So that it does not run the rest of the Awake and Update code
                internals.isGoingToDelete = true;
                if (Application.isPlaying) {
                    Destroy(gameObject);
                } 
            }
        }

        private void Awake() {
            InstanceCheck();
            if (!internals.isGoingToDelete) {
                if (Application.isPlaying) {
                    internals.AwakeCheck();
                }
            }
        }

        private void Update() {
            if (!internals.isGoingToDelete) {
#if UNITY_EDITOR
                if (Application.isPlaying) {
#endif
                    internals.ManagedUpdate();
#if UNITY_EDITOR
                }
                else {
                    InstanceCheck();
                }
#endif
            }
        }

        private void OnDestroy() {
            // So that if the SoundManager is destroyed during eg. scene switching it will stop all playing sounds
            if (Application.isPlaying) {
                internals.Destroy();
            }
        }

#if UNITY_EDITOR
        private void OnGUI() {
            // For live <see cref="SoundEvent"/> debugging
            internals.DebugInGameViewUpdate();
        }
#endif

        private void OnApplicationQuit() {
            internals.applicationIsQuitting = true;
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        public void Play(SoundEvent soundEvent, Transform owner) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.Play, owner, null, null, null, null, null, null, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play(SoundEvent soundEvent, Transform owner, SoundTag localSoundTag) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.Play, owner, null, null, null, null, null, null, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play(SoundEvent soundEvent, Transform owner, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.Play, owner, null, null, null, null, soundParameters, null, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/> (can follow positon)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play(SoundEvent soundEvent, Transform owner, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, owner, null, null), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.Play, owner, null, null, null, null, soundParameters, null, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Transform position) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The position {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The owner {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), soundEvent);
                    }
                } else {
                    if (soundEvent == null) {
                        if (ShouldDebug.Warnings()) {
                            Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                        }
                    } else {
                        internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtTransform, owner, null, position, null, null, null, null, null);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Transform position, SoundTag localSoundTag) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The position {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The owner {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), soundEvent);
                    }
                } else {
                    if (soundEvent == null) {
                        if (ShouldDebug.Warnings()) {
                            Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                        }
                    } else {
                        internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtTransform, owner, null, position, null, null, null, null, localSoundTag);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/> (can follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Transform position, params SoundParameterInternals[] soundParameters) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The position {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The owner {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), soundEvent);
                    }
                } else {
                    if (soundEvent == null) {
                        if (ShouldDebug.Warnings()) {
                            Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                        }
                    } else {
                        internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtTransform, owner, null, position, null, null, soundParameters, null, null);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Transform"/> position with another <see cref="Transform"/> owner with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
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
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Transform position, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The position {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                }
            } else {
                if (owner == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The owner {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), soundEvent);
                    }
                } else {
                    if (soundEvent == null) {
                        if (ShouldDebug.Warnings()) {
                            Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, position, owner), owner);
                        }
                    } else {
                        internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtTransform, owner, null, position, null, null, soundParameters, null, localSoundTag);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Vector3"/> position
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Vector3 position) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, null, owner), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, null, owner), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtVector, owner, position, null, null, null, null, null, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="Vector3"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Vector3 position, SoundTag localSoundTag) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, null, owner), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, null, owner), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtVector, owner, position, null, null, null, null, null, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Vector3"/> position
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Vector3"/> (can't follow position)
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Vector3 position, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null." + internals.DebugInfoString(soundEvent, null, null, owner), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null." + internals.DebugInfoString(soundEvent, null, null, owner), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtVector, owner, position, null, null, null, soundParameters, null, null);
                }
            }
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> at the <see cref="Vector3"/> position with the Local <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
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
        public void PlayAtPosition(SoundEvent soundEvent, Transform owner, Vector3 position, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null" + internals.DebugInfoString(soundEvent, null, null, owner), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null" + internals.DebugInfoString(soundEvent, null, null, owner), owner);
                    }
                } else {
                    internals.PlaySoundEvent(soundEvent, SoundEventPlayType.PlayAtVector, owner, position, null, null, null, soundParameters, null, localSoundTag);
                }
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> with the owner <see cref="Transform"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to stop
        /// </param>
        /// <param name='owner'>
        /// The owner <see cref="Transform"/>
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void Stop(SoundEvent soundEvent, Transform owner, bool allowFadeOut = true) {
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null" + internals.DebugInfoString(soundEvent, owner, null, null), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null" + internals.DebugInfoString(soundEvent, owner, null, null), owner);
                    }
                } else {
                    internals.Stop(soundEvent, owner, allowFadeOut);
                }
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> played at the position <see cref="Transform"/>.
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to stop
        /// </param>
        /// <param name='position'>
        /// The position <see cref="Transform"/>
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAtPosition(SoundEvent soundEvent, Transform position, bool allowFadeOut = true) {
            if (position == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null" + internals.DebugInfoString(soundEvent, position, null, null), soundEvent);
                }
            } else {
                if (soundEvent == null) {
                    if (ShouldDebug.Warnings()) {
                        Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null" + internals.DebugInfoString(soundEvent, position, null, null), transform);
                    }
                } else {
                    internals.StopAtPosition(soundEvent, position, allowFadeOut);
                }
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
            if (owner == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(Transform)} is null" + internals.DebugInfoString(null, owner, null, null));
                }
            } else {
                internals.StopAllAtOwner(owner, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> everywhere
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> which to stop
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopEverywhere(SoundEvent soundEvent, bool allowFadeOut = true) {
            if (soundEvent == null) {
                if (ShouldDebug.Warnings()) {
                    Debug.LogWarning($"Sonity.{nameof(SoundManager)}: The {nameof(SoundEvent)} is null" + internals.DebugInfoString(soundEvent, null, null, null));
                }
            } else {
                internals.StopEverywhere(soundEvent, allowFadeOut);
            }
        }

        /// <summary>
        /// Stops the all <see cref="SoundEvent"/>s
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/>s should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        public void StopEverything(bool allowFadeOut = true) {
            internals.StopEverything(allowFadeOut);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        public void Play2D(SoundEvent soundEvent) {
            Play(soundEvent, internals.cachedTransform2D);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with the Local <see cref="SoundTag"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        public void Play2D(SoundEvent soundEvent, SoundTag localSoundTag) {
            Play(soundEvent, internals.cachedTransform2D, localSoundTag);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play2D(SoundEvent soundEvent, params SoundParameterInternals[] soundParameters) {
            Play(soundEvent, internals.cachedTransform2D, soundParameters);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> with <see cref="SoundParameterInternals"/> with the Local <see cref="SoundTag"/> with the 2D <see cref="Transform"/> as owner
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name='localSoundTag'>
        /// The <see cref="SoundTag"/> which will determine the Local <see cref="SoundTag"/> of the <see cref="SoundEvent"/>
        /// </param>
        /// <param name='soundParameters'>
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void Play2D(SoundEvent soundEvent, SoundTag localSoundTag, params SoundParameterInternals[] soundParameters) {
            Play(soundEvent, internals.cachedTransform2D, localSoundTag, soundParameters);
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> at the 2D <see cref="Transform"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to stop
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void Stop2D(SoundEvent soundEvent, bool allowFadeOut = true) {
            Stop(soundEvent, internals.cachedTransform2D, allowFadeOut);
        }

        /// <summary>
        /// Stops all <see cref="SoundEvent"/>s at the 2D <see cref="Transform"/>
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAllAt2D(bool allowFadeOut = true) {
            StopAllAtOwner(internals.cachedTransform2D, allowFadeOut);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="SoundManager"/>s music <see cref="Transform"/>
        /// </summary>
        /// <param name="soundEvent">
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name="stopAllOtherMusic">
        /// If all other <see cref="SoundEvent"/>s played at the <see cref="SoundManager"/>s music <see cref="Transform"/> should be stopped
        /// </param>
        /// <param name="allowFadeOut">
        /// If the other stopped <see cref="SoundEvent"/> should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        public void PlayMusic(SoundEvent soundEvent, bool stopAllOtherMusic = true, bool allowFadeOut = true) {
            if (stopAllOtherMusic) {
                StopAllMusic(allowFadeOut);
            }
            Play(soundEvent, internals.cachedTransformMusic);
        }

        /// <summary>
        /// Plays the <see cref="SoundEvent"/> at the <see cref="SoundManager"/>s music <see cref="Transform"/>
        /// </summary>
        /// <param name="soundEvent">
        /// The <see cref="SoundEvent"/> to play
        /// </param>
        /// <param name="stopAllOtherMusic">
        /// If all other <see cref="SoundEvent"/>s played at the <see cref="SoundManager"/>s music <see cref="Transform"/> should be stopped
        /// </param>
        /// <param name="allowFadeOut">
        /// If the other stopped <see cref="SoundEvent"/> should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        /// <param name="soundParameters">
        /// For example <see cref="SoundParameterVolumeDecibel"/> is used to modify how the <see cref="SoundEvent"/> is played
        /// </param>
        public void PlayMusic(SoundEvent soundEvent, bool stopAllOtherMusic = true, bool allowFadeOut = true, params SoundParameterInternals[] soundParameters) {
            if (stopAllOtherMusic) {
                StopAllMusic(allowFadeOut);
            }
            Play(soundEvent, internals.cachedTransformMusic, soundParameters);
        }

        /// <summary>
        /// Stops the <see cref="SoundEvent"/> playing at the <see cref="SoundManager"/>s music <see cref="Transform"/>
        /// </summary>
        /// <param name='soundEvent'>
        /// The <see cref="SoundEvent"/> to stop
        /// </param>
        /// <param name='allowFadeOut'>
        /// If the other stopped <see cref="SoundEvent"/> should be allowed to fade out. Otherwise they are going to be stopped immediately
        /// </param>
        public void StopMusic(SoundEvent soundEvent, bool allowFadeOut = true) {
            Stop(soundEvent, internals.cachedTransformMusic, allowFadeOut);
        }

        /// <summary>
        /// Stops the all <see cref="SoundEvent"/>s playing at the <see cref="SoundManager"/>s music <see cref="Transform"/>
        /// </summary>
        /// <param name='allowFadeOut'>
        /// If the <see cref="SoundEvent"/> should be allowed to fade out. Otherwise it is going to be stopped immediately
        /// </param>
        public void StopAllMusic(bool allowFadeOut = true) {
            StopAllAtOwner(internals.cachedTransformMusic, allowFadeOut);
        }

        /// <summary>
        /// <para> If playing it returns <see cref="SoundEventState.Playing"/> </para> 
        /// <para> If not playing, but it is delayed it returns <see cref="SoundEventState.Delayed"/> </para> 
        /// <para> If not playing and it is not delayed it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// <para> If the <see cref="SoundEvent"/> or <see cref="Transform"/> is null it returns <see cref="SoundEventState.NotPlaying"/> </para> 
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the <see cref="SoundEventState"/> from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <returns> Returns <see cref="SoundEventState"/> of the <see cref="SoundEvent"/>s <see cref="SoundEventInstance"/> </returns>
        public SoundEventState GetSoundEventState(SoundEvent soundEvent, Transform owner) {
            return internals.GetSoundEventState(soundEvent, owner);
        }

        /// <summary>
        /// <para> Returns the length (in seconds) of the <see cref="AudioClip"/> in the last played <see cref="AudioSource"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// <para> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the length from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <param name="pitchSpeed"> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </param>
        /// <returns> Length in seconds </returns>
        public float GetLastPlayedClipLength(SoundEvent soundEvent, Transform owner, bool pitchSpeed) {
            return internals.GetLastPlayedClipLength(soundEvent, owner, pitchSpeed);
        }

        /// <summary>
        /// <para> Returns the current time (in seconds) of the <see cref="AudioClip"/> in the last played <see cref="AudioSource"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// <para> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the time from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <param name="pitchSpeed"> If it should be scaled by pitch. E.g. -12 semitones will be twice as long </param>
        /// <returns> Time in seconds </returns>
        public float GetLastPlayedClipTime(SoundEvent soundEvent, Transform owner, bool pitchSpeed) {
            return internals.GetLastPlayedClipTime(soundEvent, owner, pitchSpeed);
        }

        /// <summary>
        /// <para> Returns the max length (in seconds) of the <see cref="SoundEvent"/> (calculated from the longest audioClip) </para>
        /// <para> Is scaled by the pitch of the <see cref="SoundEvent"/> and <see cref="SoundContainer"/> </para>
        /// <para> Does not take into account random, intensity or parameter pitch </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> is null </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the length from </param>
        /// <returns> The max length in seconds </returns>
        public float GetMaxLength(SoundEvent soundEvent) {
            return internals.GetMaxLength(soundEvent, false);
        }

        /// <summary>
        /// <para> Returns the time (in seconds) since the <see cref="SoundEvent"/> was played </para>
        /// <para> Calculated using <see cref="Time.realtimeSinceStartup"/> </para>
        /// <para> Returns 0 if the <see cref="SoundEventInstance"/> is not playing </para>
        /// <para> Returns 0 if the <see cref="SoundEvent"/> or <see cref="Transform"/> is null </para>
        /// </summary>
        /// <param name="soundEvent"> The <see cref="SoundEvent"/> get the time played from </param>
        /// <param name="owner"> The owner <see cref="Transform"/> </param>
        /// <returns> Time in seconds </returns>
        public float GetTimePlayed(SoundEvent soundEvent, Transform owner) {
            return internals.GetTimePlayed(soundEvent, owner);
        }

        /// <summary>
        /// Sets the global <see cref="SoundTag"/>
        /// </summary>
        /// <param name='soundTag'>
        /// The <see cref="SoundTag"/> to change to
        /// </param>
        public void SetGlobalSoundTag(SoundTag soundTag) {
            internals.settings.globalSoundTag = soundTag;
        }

        /// <summary>
        /// Returns the global <see cref="SoundTag"/>
        /// </summary>
        /// <returns> The global <see cref="SoundTag"/> </returns>
        public SoundTag GetGlobalSoundTag() {
            return internals.settings.globalSoundTag;
        }

        /// <summary>
        /// Sets the global distance scale (default is a scale of 100 units)
        /// </summary>
        /// <param name='distanceScale'>
        /// The new range scale
        /// </param>
        public void SetGlobalDistanceScale(float distanceScale) {
            internals.settings.distanceScale = Mathf.Clamp(distanceScale, 0f, Mathf.Infinity);
        }

        /// <summary>
        /// Returns the global distance scale
        /// </summary>
        public float GetGlobalDistanceScale() {
            return internals.settings.distanceScale;
        }

        /// <summary>
        /// Set if speed of sound should be enabled
        /// </summary>
        /// <param name='speedOfSoundEnabled'>
        /// Should speed of Sound be active
        /// </param>
        public void SetSpeedOfSoundEnabled(bool speedOfSoundEnabled) {
            internals.settings.speedOfSoundEnabled = speedOfSoundEnabled;
        }

        /// <summary>
        /// <para> Set the speed of sound scale </para>
        /// <para> The default is a multiplier of 1 (by the base value of 340 unity units per second) </para>
        /// </summary>
        /// <param name='speedOfSoundScale'>
        /// The scale of speed of Sound (default is a multipler of 1)
        /// </param>
        public void SetSpeedOfSoundScale(float speedOfSoundScale) {
            internals.settings.speedOfSoundScale = Mathf.Clamp(speedOfSoundScale, 0f, Mathf.Infinity);
        }

        /// <summary>
        /// Returns the speed of sound scale
        /// </summary>
        public float GetSpeedOfSoundScale() {
            return internals.settings.speedOfSoundScale;
        }

        /// <summary>
        /// Sets the <see cref="Voice"/> limit
        /// </summary>
        /// <param name='voiceLimit'>
        /// The maximum number of <see cref="Voice"/>s which can be played at the same time
        /// </param>
        public void SetVoiceLimit(int voiceLimit) {
            internals.settings.voiceLimit = Mathf.Clamp(voiceLimit, 0, int.MaxValue);
        }

        /// <summary>
        /// Returns the <see cref="Voice"/> limit
        /// </summary>
        public int GetVoiceLimit() {
            return internals.settings.voiceLimit;
        }

        /// <summary>
        /// Sets the <see cref="VoiceEffect"/> limit
        /// </summary>
        public void SetVoiceEffectLimit(int voiceEffectLimit) {
            internals.settings.voiceEffectLimit = Mathf.Clamp(voiceEffectLimit, 0, int.MaxValue);
        }

        /// <summary>
        /// Returns the <see cref="VoiceEffect"/> limit
        /// </summary>
        public int GetVoiceEffectLimit() {
            return internals.settings.voiceEffectLimit;
        }

        /// <summary>
        /// Disables/enables all the Play/PlayAtPosition functionality
        /// </summary>
        /// <param name="disablePlayingSounds"></param>
        public void SetDisablePlayingSounds(bool disablePlayingSounds) {
            internals.settings.disablePlayingSounds = disablePlayingSounds;
        }

        /// <summary>
        /// If the Play/PlayAtPosition functionality is disabled
        /// </summary>
        public bool GetDisablePlayingSounds() {
            return internals.settings.disablePlayingSounds;
        }
    }
}