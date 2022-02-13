using UnityEngine;

namespace YWR.Tools
{
    public class UiGameEventListener : MonoBehaviour, IListener
    {
        protected virtual void Awake()
        {
            Subscribe();
        }

        protected virtual void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (GameEvents.Instance)
            {
                GameEvents.Instance.AddListener(this);
            }
        }

        private void Unsubscribe()
        {
            if (GameEvents.Instance)
            {
                GameEvents.Instance.RemoveListener(this);
            }
        }
    }
}