﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    /// <summary>
    ///     A Button inside the Dialog System.
    /// </summary>
    [CreateAssetMenu(menuName = "DialogSystem/TextButton")]
    public class TextButton : ScriptableObject
    {
        /// <summary>
        ///     Key attached to this button.
        /// </summary>
        public KeyCode BondedKey = KeyCode.None;

        /// <summary>
        ///     Prefab of the button.
        /// </summary>
        public GameObject PrefabButton;

        /// <summary>
        ///     The text inside the button.
        /// </summary>
        public string Text;

        /// <summary>
        ///     Callback to assign the event.
        /// </summary>
        public Dictionary<IDialogSystem, Action> OnClick = new Dictionary<IDialogSystem, Action>();

        private string KeyText { get; set; } = string.Empty;

        /// <summary>
        ///     The parent dialog.
        /// </summary>
        private IDialogSystem Dialog { get; set; }

        /// <summary>
        ///     Creates the button, assigns the necessary callbacks and texts.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public DialogButton CreateButton(Transform parent, IDialogSystem dialog)
        {
            KeyText = " [" + BondedKey + "]";
            GameObject goButton = Instantiate(PrefabButton, parent);
            DialogButton btn = goButton.GetComponent<DialogButton>();
            btn.SetText(Text + KeyText);
            btn.SetKeyCode(BondedKey);
            btn.AddListener(OnClick[dialog].Invoke);
            return btn;
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