using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(menuName = "DialogSystem/Parameters")]
    public class Parameters : ScriptableObject
    {
        [SerializeField] [Tooltip("Characters per second.")] [Range(50, 2000)]
        private int speed = 300;

        //------------------------------------------------------------------------------------------------------------
        public int Speed => speed;
    }
}