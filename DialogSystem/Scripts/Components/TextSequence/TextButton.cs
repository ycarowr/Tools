using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tools.Dialog
{
    /// <summary>
    ///     A Button inside the Dialog System.
    /// </summary>
    [CreateAssetMenu(menuName = "DialogSystem/TextButton")]
    public class TextButton : ScriptableObject
    {
        /// <summary>
        ///     The parent dialog.
        /// </summary>
        private IDialogSystem Dialog { get; set; }
        
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

        /// <summary>
        ///     Creates the button, assigns the necessary callbacks and texts.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject CreateButton(Transform parent)
        {
            var goButton = Instantiate(PrefabButton, parent);
            var btn = goButton.GetComponent<DialogButton>();
            btn.AddListener(OnPress.Invoke);
            btn.SetText(Text);
            return btn.gameObject;
        }
        
        /// <summary>
        ///     Sets the parent dialog;
        /// </summary>
        /// <param name="dialog"></param>
        public void SetDialog(IDialogSystem dialog)
        {
            Dialog = dialog;
        }

        /// <summary>
        ///     Provides access to its own dialog property Hide method.
        /// </summary>
        public void Hide()
        {
            Dialog?.Hide();
        }

        /// <summary>
        ///     Provides access to its own dialog property Next method.    
        /// </summary>
        public void Next()
        {
            Dialog?.Next();
        }
    }
}