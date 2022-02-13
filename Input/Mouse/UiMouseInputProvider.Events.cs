using System;
using UnityEngine.EventSystems;

namespace YWR.Tools
{
    public partial class UiMouseInputProvider
    {
        //TODO: Consider to implement Safe Invokes.
        Action<PointerEventData> IMouseInput.OnPointerDown { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerUp { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerClick { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnBeginDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnEndDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnDrop { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerEnter { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerExit { get; set; } = eventData => { };
    }
}