using System;
using UnityEngine;

namespace Tools
{
    public class KeyboardInput : MonoBehaviour, IKeyboardInput
    {
        [SerializeField] [Tooltip("The Keyboard key")]
        private KeyCode key;

        public KeyCode Key => key;
        public Action OnKey { get; set; } = () => { };
        public Action OnKeyDown { get; set; } = () => { };
        public Action OnKeyUp { get; set; } = () => { };
        public bool IsTracking { get; private set; }

        private void Update()
        {
            if (!IsTracking)
                return;
            
            var isKey = Input.GetKey(key);
            var isKeyDown = Input.GetKeyDown(key);
            var isKeyUp = Input.GetKeyUp(key);

            if (isKey)
                OnKey?.Invoke();
            if (isKeyDown)
                OnKeyDown?.Invoke();
            if (isKeyUp)
                OnKeyUp?.Invoke();
        }

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