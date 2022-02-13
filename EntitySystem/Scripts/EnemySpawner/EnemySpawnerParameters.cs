using UnityEngine;

namespace YWR.Tools
{
    [CreateAssetMenu]
    public class EnemySpawnerParameters : ScriptableObject
    {
        [SerializeField] private float spawnerRadius = 10;

        [SerializeField] private int amount = 1;

        [SerializeField] private float interval = 0.1f;

        [SerializeField] private GameObject enemyPrefab;

        #region Public

        public float SpawnerRadius => spawnerRadius;

        public int Amount => amount;

        public float Interval => interval;

        public GameObject EnemyPrefab => enemyPrefab;

        #endregion
    }
}