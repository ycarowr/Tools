using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(menuName = "DialogSystem/TextSequence")]
    public class TextSequence : ScriptableObject
    {
        public TextPiece[] Sequence;
    }
}