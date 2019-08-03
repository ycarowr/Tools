using System.Collections;
using System.Collections.Generic;
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