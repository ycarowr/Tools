using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace Tools
{
    public partial class DialogSystem
    {
        private class DialogWriting : DialogSubComponent
        {
            public DialogWriting(IDialogSystem system,
                TextMeshProUGUI sentence,
                TextMeshProUGUI author) : base(system)
            {
                Builder = new StringBuilder();
                AuthorText = author;
                SentenceText = sentence;
            }

            private StringBuilder Builder { get; }
            private int CharLength { get; set; }
            private Coroutine WriteRoutine { get; set; }
            private TextMeshProUGUI SentenceText { get; }
            private TextMeshProUGUI AuthorText { get; }


            public void Write(string text, string author)
            {
                CharLength = 0;
                Builder.Length = 0;
                Builder.Append(text);
                AuthorText.text = author;
                if (!DialogSystem.IsOpened)
                    DialogSystem.Show();
                else
                    StartWriting();
            }

            public void Clear()
            {
                Builder.Length = 0;
                SentenceText.text = string.Empty;
                AuthorText.text = string.Empty;
            }

            public void StartWriting()
            {
                if (WriteRoutine != null)
                {
                    DialogSystem.Monobehavior.StopCoroutine(WriteRoutine);
                    WriteRoutine = null;
                }

                if (Builder.Length <= 0)
                    return;

                StartCoroutine();
            }

            //-----------------------------------------------------------------------------------------

            private IEnumerator KeepWriting(float delay)
            {
                yield return new WaitForSeconds(delay);
                
                var aSentence = Builder.ToString();
                var subSentence = CharLength <= aSentence.Length
                    ? aSentence.Substring(0, CharLength)
                    : string.Empty;
                SentenceText.text = subSentence;

                ++CharLength;

                var hasEnded = CharLength > Builder.Length;
                if (!hasEnded)
                    StartCoroutine();
            }

            private void StartCoroutine()
            {
                var delay = CalculateTime();
                WriteRoutine = DialogSystem.Monobehavior.StartCoroutine(KeepWriting(delay));
            }

            /// <summary>
            ///     Return the time necessary to wait according to the sentence length.
            /// </summary>
            /// <returns></returns>
            private float CalculateTime()
            {
                // v = d / t
                // wordsPerSecond = totalWords/totalSeconds
                return Builder.Length / DialogSystem.Speed;
            }
        }
    }
}