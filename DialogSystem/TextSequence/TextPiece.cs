using UnityEngine;
using UnityEngine.Events;

namespace Tools.Dialog
{
    [CreateAssetMenu(menuName = "DialogSystem/TextPiece")]
    public class TextPiece : ScriptableObject
    {
        /// <summary>
        ///     Author of the text. Appears at the top.
        /// </summary>
        public string Author;
        
        /// <summary>
        ///     The text inside the box.
        /// </summary>
        [Multiline] public string Text;
        
        /// <summary>
        ///     The buttons that interact with the text.
        /// </summary>
        public TextButton[] Buttons;
    }
}