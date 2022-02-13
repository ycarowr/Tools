using System;
using UnityEngine;

namespace YWR.Tools
{
    [Serializable]
    public struct PlayerData
    {
        public ulong id;
        public Transform transform;
        public GameObject gameObject;
        public Collider collider;
        public Rigidbody rigidbody;
        public Collider triggerCollider;

        public bool IsPlayer(GameObject gameObject)
        {
            return gameObject == this.gameObject;
        }
    }
}