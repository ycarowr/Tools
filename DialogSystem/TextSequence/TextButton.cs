using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tools.Dialog
{
    [CreateAssetMenu(menuName = "DialogSystem/TextButton")]
    public class TextButton : ScriptableObject
    {
        /// <summary>
        ///     The parent dialog.
        /// </summary>
        public IDialogSystem Dialog { get; set; }
        
        /// <summary>
        ///     The text inside the button.
        /// </summary>
        public string Text;

        /// <summary>
        ///     Prefab of the button.
        /// </summary>
        public GameObject PrefabButton;
        
        /// <summary>
        ///     Callback to assign the event.
        /// </summary>
        public UnityEvent OnPress = new UnityEvent();

        public GameObject CreateButton(Transform parent)
        {
            var goButton = Instantiate(PrefabButton, parent);
            var btn = goButton.GetComponent<ButtonDialog>();
            btn.AddListener(OnPress.Invoke);
            btn.SetText(Text);
            return btn.gameObject;
        }

        public void Hide()
        {
            Dialog?.Hide();
        }

        public void Next()
        {
            Dialog?.WriteNext();
        }
    }
}