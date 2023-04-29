using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int count;

        // Start is called before the first frame update
        void Start()
        {
            Vector3[] positions = Utils.GetRandomCirclePosition(transform.position, 2f, 20f, count);
            for (int i = 0; i < count; i++)
            {
                Instantiate(prefab, positions[i], Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}