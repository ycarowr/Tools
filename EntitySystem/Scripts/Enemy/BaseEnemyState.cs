using UnityEngine;

namespace YWR.Tools
{
    public abstract class BaseEnemyState : BaseEntityState
    {
        protected BaseEnemyState(EnemyBehavioursFsm fsm) : base(fsm)
        {
            Fsm = fsm;
        }

        protected new EnemyBehavioursFsm Fsm { get; }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Data.Debug.currentState = GetType().ToString();
        }

        protected float GetDistanceFromSpawnPoint()
        {
            Vector3 spawnPointPosition = GetSpawnPosition();
            Vector3 myPosition = Data.Transform.position;
            float distance = Vector3.Distance(myPosition, spawnPointPosition);
            return distance;
        }

        protected float GetDistanceFromPlayer()
        {
            Vector3 myPosition = Data.Transform.position;
            Vector3 playerPosition = GetPlayerPosition();
            float distance = Vector3.Distance(playerPosition, myPosition);
            return distance;
        }

        protected Vector3 GetSpawnPosition()
        {
            Vector3 spawnPoint = Data.Debug.spawnPoint;
            return spawnPoint;
        }

        protected bool IsPlayerInRangeToAttack()
        {
            float range = Data.Parameters.AttackRange;
            float distance = Vector3.Distance(GetPosition(), GetPlayerPosition());
            return distance <= range;
        }

        protected bool IsPlayerInRange()
        {
            float distance = GetDistanceFromPlayer();
            return distance < Data.Parameters.FollowThreshold;
        }

        protected Vector3 GetPlayerPosition()
        {
            return GetNearestPlayer().position;
        }

        protected Transform GetNearestPlayer()
        {
            Vector3 position = GetPosition();
            PlayerProvider playerProvider = Data.PlayerProvider;
            return playerProvider.GetNearestPlayer(position).transform;
        }

        protected Vector3 GetPosition()
        {
            return Data.Transform.position;
        }

        protected void SetDestination(Vector3 destiny)
        {
            Data.NavMeshAgent.SetDestination(destiny);
        }
    }
}