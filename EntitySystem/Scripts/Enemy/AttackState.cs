using System.Collections;
using UnityEngine;

namespace YWR.Tools
{
    public class AttackState : BaseEnemyState
    {
        private Coroutine m_AttackCoroutine;
        private bool m_IsAttacking;

        public AttackState(EnemyBehavioursFsm fsm) : base(fsm)
        {
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            if (!m_IsAttacking)
            {
                PlayerData currentPlayer = Data.PlayerProvider.GetNearestPlayer(GetPosition());
                GameObject playerGameObject = currentPlayer.gameObject;
                Data.BaseComponent.StartCoroutine(ApplyDamage(playerGameObject));
                m_IsAttacking = true;
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            m_IsAttacking = false;
            if (m_AttackCoroutine != null)
            {
                Data.BaseComponent.StopCoroutine(m_AttackCoroutine);
                m_AttackCoroutine = null;
            }
        }

        private IEnumerator ApplyDamage(GameObject playerGameObject)
        {
            yield return new WaitForSeconds(Data.Parameters.AttackSpeed);

            if (playerGameObject == null)
            {
                yield break;
            }

            CompleteDamageCycle(playerGameObject);
        }

        private void CompleteDamageCycle(GameObject playerGameObject)
        {
            if (IsPlayerInRangeToAttack())
            {
                m_AttackCoroutine = Data.BaseComponent.StartCoroutine(ApplyDamage(playerGameObject));
            }
            else
            {
                if (m_AttackCoroutine != null)
                {
                    Data.BaseComponent.StopCoroutine(m_AttackCoroutine);
                    m_AttackCoroutine = null;
                }

                if (IsPlayerInRange())
                {
                    Fsm.Follow();
                }
                else
                {
                    Fsm.Idle();
                }
            }
        }
    }
}