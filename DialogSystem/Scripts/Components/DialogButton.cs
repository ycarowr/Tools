using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Dialog
{
    public class DialogButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private KeyboardInput keyBoard;

        private void Awake()
        {
            keyBoard.OnKeyDown += button.onClick.Invoke;
            keyBoard.StartTracking();
        }
        
        public void SetText(string txt)
        {
            tmpText.text = txt;
        }

        public void SetKeyCode(KeyCode key)
        {
            keyBoard.SetKey(key);
        }

        public void AddListener(Action action)
        {
            if (action == null) 
                return;
            
            button.onClick.AddListener(action.Invoke);
        }
    }
}