using UnityEngine;
using Sonity;

namespace ExampleSonity {

    [AddComponentMenu("")]
    public class ExamplePlay : MonoBehaviour {

        public SoundEvent soundEvent;

        void Update() {

            // Plays the sound on left mouse click
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                soundEvent.Play(transform);
            }
            // Stops the sound on right mouse click
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                soundEvent.Stop(transform);
            }
        }
    }
}