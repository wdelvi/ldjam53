using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public class DataManager : MonoBehaviour
    {
        [Header("Resources Sub Folder")]
        public string loadFolder = "";

        private static DataManager instance;

        private void Awake()
        {
            instance = this;

            Load();
        }

        private void Load()
        {
            AIAction.Load(loadFolder);
        }
    }
}