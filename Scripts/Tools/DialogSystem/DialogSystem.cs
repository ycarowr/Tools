using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public partial class DialogSystem : MonoBehaviour, IDialogSystem
    {
        [SerializeField] private TextMeshProUGUI authorText;

        [Header("Set by Editor")] [SerializeField]
        private GameObject content;

        [SerializeField] private Button NextButton;
        [SerializeField] private Parameters parameters;
        [SerializeField] private TextMeshProUGUI sentenceText;
        private IKeyboardInput Keyboard { get; set; }

        private DialogAnimation Animation { get; set; }
        private DialogWriting Writing { get; set; }
        private DialogSequence Sequence { get; set; }


        public int Speed => parameters.Speed;
        public bool IsOpened { get; private set; }
        public Action OnShow { get; set; } = () => { };
        public Action OnHide { get; set; } = () => { };
        public Action OnFinishSequence { get; set; } = () => { };
        public MonoBehaviour Monobehavior => this;

        protected void Awake()
        {
            Animation = new DialogAnimation(this);
            Writing = new DialogWriting(this, sentenceText, authorText);
            Sequence = new DialogSequence(this);
            Clear();
            Hide();
            Keyboard = GetComponent<IKeyboardInput>();
            Keyboard.OnKeyDown += PressNext;
            NextButton.onClick.AddListener(PressNext);
        }


        //-----------------------------------------------------------------------------------------

        private void PressNext()
        {
            if (!IsOpened)
                return;

            if (Sequence == null)
                return;

            if (Sequence.IsLast)
                OnFinishSequence?.Invoke();

            var current = Sequence.GetCurrent();
            if (current == null)
                return;

            current.OnNext?.Invoke();
            var action = current.OnPressNext;
            switch (action)
            {
                case DialogAutoAction.Hide:
                    Hide();
                    break;
                case DialogAutoAction.Show:
                    Show();
                    break;
                case DialogAutoAction.Clear:
                    Clear();
                    break;
                case DialogAutoAction.Next:
                    WriteNext();
                    break;
            }
        }

        //-----------------------------------------------------------------------------------------

        #region Write and Clear

        [Header("Test")] [SerializeField] private TextSequence testSequence;

        [Button]
        public void Write()
        {
            Write(testSequence);
        }


        public void Write(TextSequence textSequence)
        {
            Sequence.SetSequence(textSequence);
            var current = Sequence.GetCurrent();
            if (current == null)
                return;

            var author = current.Author;
            var text = current.Text;
            Write(text, author);
        }

        public void Write(string text, string author)
        {
            Writing.Write(text, author);
        }

        [Button]
        public void WriteNext()
        {
            var next = Sequence.GetNext();
            if (next == null)
                return;

            var author = next.Author;
            var text = next.Text;
            Write(text, author);
        }

        [Button]
        public void Clear()
        {
            OnHide += Clear;
            OnShow += Writing.StartWriting;
            Writing.Clear();
        }

        #endregion

        //-----------------------------------------------------------------------------------------

        #region Show and Hide

        [Button]
        public void Show()
        {
            Animation.Show();
            IsOpened = true;
        }

        [Button]
        public void Hide()
        {
            Animation.Hide();
            Sequence.Hide();

            IsOpened = false;
        }

        #endregion

        //-----------------------------------------------------------------------------------------

        #region Activate and Deactivate

        [Button]
        public void Activate()
        {
            content.SetActive(true);
        }

        [Button]
        public void Deactivate()
        {
            content.SetActive(false);
            Hide();
        }

        #endregion


        //-----------------------------------------------------------------------------------------
    }
}