using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YWR.Tools
{
    public class DialogButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private KeyboardInput keyBoard;
        [SerializeField] private TMP_Text tmpText;

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
            {
                return;
            }

            button.onClick.AddListener(action.Invoke);
        }
    }
}