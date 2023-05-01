using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutscenePanelController : MonoBehaviour
{
    public bool showCutscene = true;
    public List<Sprite> cutsceneImages;
    public Image cutsceneImageBox;
    public GameObject cutsceneObject;

    private int imageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneObject.gameObject.SetActive(showCutscene);
        cutsceneImageBox.sprite = cutsceneImages[imageIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            imageIndex++;

            if(imageIndex > cutsceneImages.Count - 1)
            {
                cutsceneObject.gameObject.SetActive(false);
                return;
            }

            cutsceneImageBox.sprite = cutsceneImages[imageIndex];
        }
    }
}
