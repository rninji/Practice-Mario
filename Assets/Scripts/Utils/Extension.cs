using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class Extension
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }

    public static void AddToggleEvent(this GameObject go, UnityAction<bool> action)
    {
        UI_Base.BindToggleEvent(go, action);
    }

    public static void AddSliderEvent(this GameObject go, UnityAction<float> action)
    {
        UI_Base.BindSliderEvent(go, action);
    }
}
