// http://www.sonity.org/ Created by Victor Engström
// Copyright 2022 Sonigon AB - All Rights Reserved

using UnityEngine;
using Sonity.Internal;

namespace Sonity {

    /// <summary>
    /// <see cref="SoundPhysics"/> is a component used for easily playing <see cref="SoundEvent"/>s on physics collisions and friction.
    /// Use intensity record in the <see cref="SoundContainer"/> for easy scaling of the velocity into a 0 to 1 range.
    /// All <see cref="SoundPhysics"/> components are multi-object editable.
    /// </summary>
    [AddComponentMenu("Sonity/Sonity - Sound Physics")]
    public class SoundPhysics : MonoBehaviour {

        public SoundPhysicsInternals internals = new SoundPhysicsInternals();

        private void Start() {
            internals.FindComponents(gameObject);
        }

        private void Awake() {
            internals.FindComponents(gameObject);
        }

        private void OnCollisionEnter(Collision collision) {
            internals.OnCollisionEnter(collision);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            internals.OnCollisionEnter2D(collision);
        }

        private void OnCollisionStay(Collision collision) {
            internals.OnCollisionStay(collision);
        }

        private void OnCollisionStay2D(Collision2D collision) {
            internals.OnCollisionStay2D(collision);
        }

        private void OnCollisionExit(Collision collision) {
            internals.OnCollisionExit(collision);
        }

        private void OnCollisionExit2D(Collision2D collision) {
            internals.OnCollisionExit2D(collision);
        }
    }
}