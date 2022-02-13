using System;
using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    [Serializable]
    public class EntityDebug
    {
        public Vector3 finalPosition;
        public Vector3 spawnPoint;
        public string currentState;
        public List<Vector3> idlePoints;
    }
}