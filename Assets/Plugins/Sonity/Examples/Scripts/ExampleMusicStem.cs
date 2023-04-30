using UnityEngine;
using Sonity;

namespace ExampleSonity {

    [AddComponentMenu("")]
    public class ExampleMusicStem : MonoBehaviour {

        public SoundEvent musicStem;
        private SoundParameterIntensity parameterIntensity = new SoundParameterIntensity(0f, UpdateMode.Continuous);

        private void Start() {
            musicStem.PlayMusic(true, true, parameterIntensity);
        }

        private void Update() {

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                // Set intensity to low
                parameterIntensity.Intensity = 0f;
            } else if (Input.GetKeyDown(KeyCode.Mouse1)) {
                // Set intensity to high
                parameterIntensity.Intensity = 1f;
            }

            // Setting gui text
            if (parameterIntensity.Intensity > 0.5f) {
                GetComponent<ExampleHelperGuiText>().textString = "Press left/right mouse buttons\nto change from high to low intensity\nCurrently it is " + "high intensity";
            } else {
                GetComponent<ExampleHelperGuiText>().textString = "Press left/right mouse buttons\nto change from high to low intensity\nCurrently it is " + "low intensity";
            }
        }
    }
}