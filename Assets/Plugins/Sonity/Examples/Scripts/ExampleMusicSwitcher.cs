using UnityEngine;

namespace ExampleSonity {

    [AddComponentMenu("")]
    public class ExampleMusicSwitcher : MonoBehaviour {

        private void Update() {

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                // Play main menu music
                SonityTemplate.TemplateSoundMusicManager.Instance.PlayMainMenu();
            } else if (Input.GetKeyDown(KeyCode.Mouse1)) {
                // Play ingame music
                SonityTemplate.TemplateSoundMusicManager.Instance.PlayIngame();
            }

            // Setting gui text
            GetComponent<ExampleHelperGuiText>().textString = "Press left/right mouse buttons\nto play main menu/ingame music\nCurrent music is " + SonityTemplate.TemplateSoundMusicManager.Instance.GetMusicPlaying();
        }
    }

}