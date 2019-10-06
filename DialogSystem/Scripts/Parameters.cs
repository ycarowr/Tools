using UnityEngine;

namespace Tools.DialogSystem
{
    [CreateAssetMenu(menuName = "DialogSystem/Parameters")]
    public class Parameters : ScriptableObject
    {
        //------------------------------------------------------------------------------------------------------------
        [field: SerializeField]
        [field: Tooltip("Characters per second.")]
        [field: Range(50, 2000)]
        public int Speed { get; } = 300;
    }
}