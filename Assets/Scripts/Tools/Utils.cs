using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace TooLoo
{
    public static class Utils
    {
        public static string WeightedSampling(IEnumerable<string> choices, IEnumerable<float> weights)
        {
            var cumulativeWeights = new List<float>();
            float sum = 0;
            foreach (var cur in weights)
            {
                sum += cur;
                cumulativeWeights.Add(sum);
            }

            float randomWeight = Random.Range(0, sum);
            int i = 0;
            foreach (var cur in choices)
            {
                if (randomWeight < cumulativeWeights[i])
                {
                    return cur;
                }
                i++;
            }
            return null;
        }

        /// <summary>
        /// Randomly selects item based on their given weights. Weights must add up to 1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="weightedItems"></param>
        /// <returns></returns>
        public static T WeightedSampling<T>(Dictionary<T, float> weightedItems)
            where T : struct
        {
            var cumulativeWeights = new List<float>();
            float sum = 0;
            foreach (KeyValuePair<T, float> item in weightedItems)
            {
                sum += item.Value;
                cumulativeWeights.Add(sum);
            }

            float randomWeight = Random.Range(0, sum);
            int i = 0;
            foreach (KeyValuePair<T, float> item in weightedItems)
            {
                if (randomWeight < cumulativeWeights[i])
                {
                    return item.Key;
                }
                i++;
            }
            return default;
        }

        /// <summary>
        /// Return a random position on a flat plane navmesh
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public static Vector3 GetRandomNavMeshPosition(Vector3 sourcePos, float maxDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance + sourcePos;
            Vector3 finalPosition = sourcePos;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 4f, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        /// <summary>
        /// Return a random navmesh position on an uneven terrain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="maxDistance"></param>
        /// <param name="terrainLayerMask"></param>
        /// <param name="areaMask"></param>
        /// <returns></returns>
        public static Vector3 GetRandomNavMeshPosition(Vector3 source, float maxDistance, LayerMask terrainLayerMask, int areaMask = NavMesh.AllAreas)
        {
            Vector3 randomPosition = source;
            Vector2 randomCirclePoint = Random.insideUnitCircle * maxDistance;
            Vector3 randomDirection = new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);
            Vector3 targetPosition = source + randomDirection;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPosition, out hit, maxDistance, areaMask))
            {
                // Perform a raycast from above the hit position to check for overlapping terrains
                RaycastHit raycastHit;
                Vector3 raycastOrigin = hit.position + Vector3.up * 100;
                if (Physics.Raycast(raycastOrigin, Vector3.down, out raycastHit, 200f, terrainLayerMask))
                {
                    // Check if the hit point is on the same terrain as the random NavMesh position
                    if (Mathf.Abs(raycastHit.point.y - hit.position.y) <= 0.5f)
                    {
                        randomPosition = hit.position;
                        return randomPosition;
                    }
                }
            }

            return randomPosition;
        }

        public static Vector3[] GetRandomCirclePosition(Vector3 sourcePos, float minRadius, float maxRadius, int count)
        {
            Vector3[] positions = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                positions[i] = new Vector3(
                    sourcePos.x + RandomNegOrPos() * (minRadius + Random.Range(0f, maxRadius - minRadius)) * Mathf.Cos(2 * Mathf.PI * ((float)i / count)),
                    0,
                    sourcePos.z + RandomNegOrPos() * (minRadius + Random.Range(0f, maxRadius - minRadius)) * Mathf.Sin(2 * Mathf.PI * ((float)i / count))
                    );
            }
            return positions;
        }

        public static Vector3 GetRandomCirclePosition(Vector3 sourcePos, float minRadius, float maxRadius)
        {
            Vector3 position = new();

            position = new Vector3(
                sourcePos.x + RandomNegOrPos() * (minRadius + Random.Range(0f, maxRadius - minRadius)) * Mathf.Cos(Random.Range(0, 2) * Mathf.PI),
                0,
                sourcePos.z + RandomNegOrPos() * (minRadius + Random.Range(0f, maxRadius - minRadius)) * Mathf.Sin(Random.Range(0, 2) * Mathf.PI)
                );

            return position;
        }

        public static Vector3[] GenerateEvenCirclePositions(Vector3 source, float radius, int count)
        {
            Vector3[] positions = new Vector3[count];
            float angleIncrement = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = i * angleIncrement * Mathf.Deg2Rad;
                Vector3 position = new Vector3(source.x + radius * Mathf.Cos(angle), source.y, source.z + radius * Mathf.Sin(angle));
                positions[i] = position;
            }

            return positions;
        }


        public static int RandomNegOrPos()
        {
            return (Random.Range(0, 2) * 2 - 1);
        }

        public static T GetNearest<T>(Vector3 source, List<T> things) where T : MonoBehaviour
        {
            float distance = Mathf.Infinity;
            T nearest = null;
            foreach (T t in things)
            {
                float distFromSource = Vector3.Distance(source, t.transform.position);
                if (distFromSource < distance)
                {
                    nearest = t;
                    distance = distFromSource;
                }
            }

            return nearest;
        }

        public static bool IsInFOV(Transform source, Transform target, float minAngle, float maxAngle)
        {
            Vector3 targetDirection = target.position - source.position;
            float angle = Vector3.Angle(targetDirection, source.forward);
            if (angle > minAngle && angle < maxAngle)
            {
                return true;
            }
            return false;
        }

        public static List<T> SortByDistance<T>(List<T> items, Vector3 position)
            where T : MonoBehaviour
        {
            return items.OrderBy(i => Vector3.SqrMagnitude(i.transform.position - position)).ToList();
        }
    }
}