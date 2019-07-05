using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Tools.Dialog
{
    public class ButtonDialog : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text tmpText;

        public void SetText(string txt)
        {
            tmpText.text = txt;
        }

        public void AddListener(Action action)
        {
            if(action != null)
                button.onClick.AddListener(action.Invoke);
        }
    }
}