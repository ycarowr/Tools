using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    public static class EnemyRegistry
    {
        public static long ID;

        public static long GetId()
        {
            return ++ID;
        }
    }

    public class EnemySpawner : SimpleStateInteractable
    {
        [SerializeField] private EnemySpawnerParameters parameters;

        [SerializeField] private SphereCollider radiusSphere;

        [SerializeField] private bool autoInitialize;

        private readonly List<GameObject> m_Registry = new List<GameObject>();

        private bool IsInitialized { get; set; }

        private void Awake()
        {
            if (radiusSphere != null)
            {
                radiusSphere.radius = parameters.SpawnerRadius;
            }

            if (autoInitialize)
            {
                SpawnEnemies();
            }
        }

        protected override void OnStartProcessing()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            if (IsInitialized)
            {
                return;
            }

            if (parameters == null)
            {
                return;
            }

            IsInitialized = true;

            for (int i = 0; i < parameters.Amount; i++)
            {
                StartCoroutine(SpawnEnemy(i * parameters.Interval));
            }
        }

        private IEnumerator SpawnEnemy(float seconds)
        {
            if (parameters == null)
            {
                yield break;
            }

            yield return new WaitForSeconds(seconds);

            Vector3 position = GetRandomPosition();
            GameObject children = Instantiate(parameters.EnemyPrefab, position, Quaternion.identity, transform);
            const string nameFormat = "Enemy_";
            children.name = nameFormat + EnemyRegistry.GetId();
            m_Registry.Add(children);
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 random = Random.insideUnitSphere * (parameters.SpawnerRadius / 3);
            Vector3 position = transform.position;
            Vector3 finalPosition = random + position;
            finalPosition.y = position.y;
            return finalPosition;
        }
    }
}