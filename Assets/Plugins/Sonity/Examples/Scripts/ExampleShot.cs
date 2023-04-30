using UnityEngine;
using Sonity;

namespace ExampleSonity {

    [AddComponentMenu("")]
    public class ExampleShot : MonoBehaviour {

        public SoundEvent soundEventShot;

        void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                soundEventShot.Play(transform);
            }
        }
    }
}
