using UnityEngine;

namespace YWR.Tools
{
    /// <summary>
    ///     A ordered sequence of <see cref="TextPiece" />.
    /// </summary>
    [CreateAssetMenu(menuName = "DialogSystem/TextSequence")]
    public class TextSequence : ScriptableObject
    {
        public TextPiece[] Sequence;
    }
}