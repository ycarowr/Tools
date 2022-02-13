using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    public class IdleState : BaseEnemyState
    {
        private readonly float m_MaxIdleTime = 2;
        private readonly List<Vector3> m_Points = new List<Vector3>();
        private int m_CurrentPositionIndex;
        private bool m_HasTarget;
        private float m_IdleTime;
        private Vector3 m_LastPosition;
        private Vector3 m_Target;

        public IdleState(EnemyBehavioursFsm fsm) : base(fsm)
        {
            m_CurrentPositionIndex = 0;
            GenerateIdlePoints();
            Data.Debug.idlePoints = m_Points;
            Data.NavMeshAgent.speed = Data.Parameters.Speed;
        }

        private void GenerateIdlePoints()
        {
            float idleRadius = Data.Parameters.IdleRadius;
            for (int i = 0; i < Data.Parameters.IdlePoints; i++)
            {
                Vector3 random = Random.insideUnitSphere * idleRadius;
                random.y = 1;
                Vector3 point = random + Data.Transform.position;
                m_Points.Add(point);
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            UpdateTarget();
        }

        public override void OnFixedUpdate()
        {
            base.OnUpdate();
            Patrol();
            if (IsPlayerInRange())
            {
                Fsm.Follow();
            }
        }

        protected virtual void Patrol()
        {
            bool isNearTarget = IsNearTarget();
            if (isNearTarget)
            {
                m_HasTarget = false;
            }

            if (!m_HasTarget)
            {
                UpdateAndClampPositionIndex();
                UpdateTarget();
            }
        }

        private bool IsNearTarget()
        {
            if (m_LastPosition == Data.Transform.position)
            {
                m_IdleTime += Time.fixedDeltaTime;
            }

            m_LastPosition = Data.Transform.position;
            if (m_IdleTime > m_MaxIdleTime)
            {
                // #ywr: hack some enemies are getting stuck because of the terrain. Fix coming in another dimension
                return true;
            }

            float distance = GetDistanceFromCurrentPositionIndex();
            return distance < EntityParameters.SMALL_FLOAT_VALUE;
        }

        private float GetDistanceFromCurrentPositionIndex()
        {
            if (m_CurrentPositionIndex >= m_Points.Count)
            {
                GenerateIdlePoints();
            }

            Vector3 point = m_Points[m_CurrentPositionIndex];
            if (Data == null)
            {
                Debug.Log("Data was null");
            }

            return Vector3.Distance(point, Data.Transform.position);
        }

        protected void UpdateTarget()
        {
            m_IdleTime = 0;
            m_Target = m_Points[m_CurrentPositionIndex];
            Data.Debug.finalPosition = m_Target;
            SetDestination(m_Target);
            m_HasTarget = true;
        }

        private void UpdateAndClampPositionIndex()
        {
            ++m_CurrentPositionIndex;
            if (m_CurrentPositionIndex > m_Points.Count - 1)
            {
                m_CurrentPositionIndex = 0;
            }
        }

        public List<Vector3> GetIdlePoints()
        {
            return m_Points;
        }
    }
}