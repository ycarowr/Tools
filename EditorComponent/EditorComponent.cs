using UnityEngine;

namespace Tools
{
    [ExecuteInEditMode]
    public class EditorComponent : MonoBehaviour
    {
        protected void OnEnable()
        {
            if (!Application.isEditor)
                Destroy(this);
        }
    }
}