using UnityEngine;

namespace YWR.Tools
{
    /// <summary> Class handles the collision detection with Player</summary>
    [RequireComponent(typeof(Collider))]
    public abstract class BaseInteractable : MonoBehaviour
    {
        [SerializeField] protected PlayerProvider playerProvider;

        private bool IsPlayer(GameObject entity)
        {
            return playerProvider.IsPlayer(entity);
        }

        #region Handle Collision

        protected void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.gameObject;
            if (IsPlayer(obj))
            {
                OnCollisionEnterPlayer(obj);
            }
        }

        protected void OnCollisionStay(Collision collision)
        {
            GameObject obj = collision.gameObject;
            if (IsPlayer(obj))
            {
                OnCollisionStayPlayer(obj);
            }
        }

        protected void OnCollisionExit(Collision collision)
        {
            GameObject obj = collision.gameObject;
            if (IsPlayer(obj))
            {
                OnCollisionExitPlayer(obj);
            }
        }

        protected virtual void OnTriggerEnter(Collider aCollider)
        {
            GameObject obj = aCollider.gameObject;
            if (IsPlayer(obj))
            {
                OnTriggerEnterPlayer(obj);
            }
        }

        protected virtual void OnTriggerExit(Collider aCollider)
        {
            GameObject obj = aCollider.gameObject;
            if (IsPlayer(obj))
            {
                OnTriggerExitPlayer(obj);
            }
        }

        #endregion

        #region Player

        protected virtual void OnCollisionEnterPlayer(GameObject obj)
        {
        }

        protected virtual void OnCollisionStayPlayer(GameObject obj)
        {
        }

        protected virtual void OnCollisionExitPlayer(GameObject obj)
        {
        }

        protected virtual void OnTriggerEnterPlayer(GameObject obj)
        {
        }

        protected virtual void OnTriggerExitPlayer(GameObject obj)
        {
        }

        #endregion
    }
}