using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Settings : UI_Popup
{
    enum Objects
    {
        Mute,
        Volume,
    }

    enum Buttons
    {
        Close,
    }

    public override void Init()
    {
        base.Init();
        
        Bind<GameObject>(typeof(Objects));
        Bind<Button>(typeof(Buttons));
        
        // 이벤트 바인딩
        GetButton((int)Buttons.Close).gameObject.AddUIEvent(OnCloseClick);
        Get<GameObject>((int)Objects.Mute).gameObject.AddToggleEvent(OnMuteClick);
        Get<GameObject>((int)Objects.Volume).gameObject.AddSliderEvent(OnVolumeDrag);
    }
    public void OnCloseClick(PointerEventData data)
    {
        // 팝업 닫기
        Managers.UI.ClosePopupUI(this);
    }

    public void OnMuteClick(bool isOn)
    {
        // 뮤트
        Debug.Log($"Muste {isOn}");
    }

    public void OnVolumeDrag(float value)
    {
        // 볼륨 조절
        Debug.Log($"Volume {value}");
    }
}
