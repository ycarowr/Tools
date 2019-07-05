using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Dialog
{
    public partial class DialogSystem : MonoBehaviour, IDialogSystem
    {
        [Header("Set by Editor")] 
        [SerializeField] private TextButton buttonNext;
        [SerializeField] private GameObject content;
        [SerializeField] private Parameters parameters;
        [SerializeField] private TextMeshProUGUI authorText;
        [SerializeField] private TextMeshProUGUI sentenceText;
        [SerializeField] private Transform buttonsAnchor;

        private List<GameObject> CurrentButtons = new List<GameObject>();
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
            buttonNext.OnPress.AddListener(PressNext);
            Animation = new DialogAnimation(this);
            Writing = new DialogWriting(this, sentenceText, authorText);
            Sequence = new DialogSequence(this);
            Keyboard = GetComponent<IKeyboardInput>();
            Keyboard.OnKeyDown += PressNext;
            OnShow += Writing.StartWriting;
            OnShow += () => CreateButtons(Sequence.GetCurrent());
            Hide();
        }


        //-----------------------------------------------------------------------------------------

        private void PressNext()
        {
            if (!IsOpened)
                return;

            if (Sequence == null)
                return;

            if (Sequence.IsLast)
            {
                OnFinishSequence?.Invoke();
                Hide();
                return;
            }

            var current = Sequence.GetCurrent();
            if (current == null)
                return;

            Clear();
            WriteNext();
        }

        #region Write and Clear

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
        
        [Button]
        public void WriteNext()
        {
            var next = Sequence.GetNext();
            if (next == null)
                return;

            var author = next.Author;
            var text = next.Text;
            CreateButtons(next);
            Write(text, author);
        }

        private void Write(string text, string author)
        {
            Writing.Write(text, author);
        }
        
        private void CreateButtons(TextPiece next)
        {
            foreach (var piece in next.Buttons)
            {
                var btn = piece.CreateButton(buttonsAnchor);
                CurrentButtons.Add(btn);
            }
        }

        [Button]
        public void Clear()
        {
            ClearButtons();
            Writing.Clear();
        }
        
        private void ClearButtons()
        {
            for (var i = 0; i < CurrentButtons.Count; i++)
                Destroy(CurrentButtons[i]);
            CurrentButtons.Clear();
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
            Clear();
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