using UnityEngine;

namespace YWR.Tools
{
    public class FollowState : BaseEnemyState
    {
        public FollowState(EnemyBehavioursFsm fsm) : base(fsm)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnUpdate();
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            Vector3 target = GetPlayerPosition();
            Data.Debug.finalPosition = target;
            SetDestination(target);

            if (!IsPlayerInRange())
            {
                Fsm.Idle();
            }
            else if (IsPlayerInRangeToAttack())
            {
                Fsm.Attack();
            }
        }
    }
}