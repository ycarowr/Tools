using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    public class EnemyComponent : BaseFsmEntity
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;

            Gizmos.DrawWireSphere(position, Data.Parameters.FollowThreshold);
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(position, Data.Parameters.IdleRadius);
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(position, Data.Parameters.AttackRange);
            Gizmos.color = Color.green;

            List<Vector3> idlePoints = Data.Debug.idlePoints;
            if (idlePoints != null)
            {
                foreach (Vector3 point in idlePoints)
                {
                    Gizmos.DrawSphere(point, 0.5f);
                }
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(Data.Debug.finalPosition, 0.5f);
        }

        protected override EntityBehavioursFsm CreateEntityFsm()
        {
            return new EnemyBehavioursFsm(Data);
        }
    }
}