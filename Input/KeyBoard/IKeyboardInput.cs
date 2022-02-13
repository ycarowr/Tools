using System;
using UnityEngine;

namespace YWR.Tools
{
    public interface IKeyboardInput
    {
        bool IsTracking { get; }
        KeyCode Key { get; }
        Action OnKey { get; set; }
        Action OnKeyDown { get; set; }
        Action OnKeyUp { get; set; }
        void StartTracking();
        void StopTracking();
    }
}