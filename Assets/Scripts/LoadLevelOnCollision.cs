using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YATE
{
    public class LoadLevelOnCollision : MonoBehaviour
    {
        public LevelManager levelManager;
        public string levelToLoad;

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterController>())
            {
                levelManager.LoadScene(levelToLoad);
            }
        }
    }
}
