using UnityEngine;
using Sonity;

namespace ExampleSonity {

    [AddComponentMenu("")]
    public class ExampleParameter : MonoBehaviour {

        public SoundEvent soundEvent;
        public SoundParameterPitchSemitone parameterPitch = new SoundParameterPitchSemitone(0f, UpdateMode.Continuous);

        void Update() {

            if (Input.GetKeyDown(KeyCode.Mouse0)) {

                // SoundParameter lower pitch
                parameterPitch.PitchSemitone -= 1f;

                // Play Sound with the parameter
                soundEvent.Play(transform, parameterPitch);
            } else if (Input.GetKeyDown(KeyCode.Mouse1)) {

                // SoundParameter increase pitch
                parameterPitch.PitchSemitone += 1f;

                // Play Sound with the parameter
                soundEvent.Play(transform, parameterPitch);
            }

            // Setting gui text
            GetComponent<ExampleHelperGuiText>().textString = "Press mouse left/right to increase/lower pitch\nCurrent pitch is " + parameterPitch.PitchSemitone.ToString("0") + " semitones";
        }
    }
}