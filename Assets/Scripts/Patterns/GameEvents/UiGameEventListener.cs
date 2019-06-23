using UnityEngine;

namespace Patterns.GameEvents
{
    public class UiGameEventListener : MonoBehaviour, IListener
    {
        protected virtual void Start()
        {
            //subscribe
            if (GameEvents.Instance)
                GameEvents.Instance.AddListener(this);
        }

        protected virtual void OnDestroy()
        {
            //unsubscribe
            if (GameEvents.Instance)
                GameEvents.Instance.RemoveListener(this);
        }
    }
}