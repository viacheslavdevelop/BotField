using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }
}