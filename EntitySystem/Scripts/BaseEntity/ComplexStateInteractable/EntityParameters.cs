using UnityEngine;

namespace YWR.Tools
{
    [CreateAssetMenu]
    public class EntityParameters : ScriptableObject
    {
        public const float SMALL_FLOAT_VALUE = 0.5f;

        [SerializeField] private float attackRange;
        [SerializeField] private float speed;
        [SerializeField] private float followThreshold;
        [SerializeField] private float idleRadius;
        [SerializeField] private int idlePoints = 2;
        [SerializeField] private float stunTime;
        [SerializeField] private bool is2D = true;
        [SerializeField] private bool isDebug = true;
        [SerializeField] private float attackSpeed = 1;
        [SerializeField] private float damage = 20;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float ballDamage = 20;
        [SerializeField] private float spawnTime = 2;
        [SerializeField] private float deadTime = 2;

        #region Public

        public float Speed => speed;

        public float AttackRange => attackRange;

        public float FollowThreshold => followThreshold;

        public float IdleRadius => idleRadius;

        public int IdlePoints => idlePoints;

        public float StunTime => stunTime;

        public float SpawnTime => spawnTime;

        public float DeadTime => deadTime;

        public float AttackSpeed => attackSpeed;

        #endregion
    }
}