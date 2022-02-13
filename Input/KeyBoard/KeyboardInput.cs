using System;
using UnityEngine;

namespace YWR.Tools
{
    public class KeyboardInput : MonoBehaviour, IKeyboardInput
    {
        [SerializeField] private KeyCode key;

        private void Update()
        {
            if (!IsTracking)
            {
                return;
            }

            bool isKey = Input.GetKey(key);
            bool isKeyDown = Input.GetKeyDown(key);
            bool isKeyUp = Input.GetKeyUp(key);

            if (isKey)
            {
                OnKey?.Invoke();
            }

            if (isKeyDown)
            {
                OnKeyDown?.Invoke();
            }

            if (isKeyUp)
            {
                OnKeyUp?.Invoke();
            }
        }

        public bool IsTracking { get; private set; }
        KeyCode IKeyboardInput.Key => key;

        public Action OnKey { get; set; } = () => { };
        public Action OnKeyDown { get; set; } = () => { };
        public Action OnKeyUp { get; set; } = () => { };

        public void StartTracking()
        {
            IsTracking = true;
        }

        public void StopTracking()
        {
            IsTracking = false;
        }

        public void SetKey(KeyCode keyCode)
        {
            key = keyCode;
        }
    }
}