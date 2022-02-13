using System;
using UnityEngine;
using UnityEngine.AI;

namespace YWR.Tools
{
    /// <summary> All necessary data to initialize an entity. </summary>
    [Serializable]
    public sealed class EntityData
    {
        [SerializeField] private EntityDebug debug;
        [SerializeField] private PlayerProvider playerProvider;
        [SerializeField] private BaseInteractable baseComponent;
        [SerializeField] private EntityParameters parameters;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ShakeAnimation shake;

        public PlayerProvider PlayerProvider => playerProvider;
        public BaseInteractable BaseComponent => baseComponent;

        public Transform Transform => baseComponent.transform;

        public GameObject GameObject => baseComponent.gameObject;

        public EntityParameters Parameters => parameters;

        public NavMeshAgent NavMeshAgent => navMeshAgent;

        public EntityDebug Debug => debug;

        public ShakeAnimation Shake => shake;
    }
}