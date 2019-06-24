using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    [CreateAssetMenu(menuName = "DialogSystem/TextPiece")]
    public class TextPiece : ScriptableObject
    {
        public string Author;
        public UnityEvent OnNext = new UnityEvent();
        public DialogSystem.DialogAutoAction OnPressNext = DialogSystem.DialogAutoAction.Next;
        [Multiline] public string Text;
    }
}