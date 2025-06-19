using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Home : UI_Scene
{
    enum Buttons
    {
        Start,
        Settings,
        Exit
    }

    public override void Init()
    {
        base.Init();
        
        Bind<Button>(typeof(Buttons));
        
        // 버튼들에 이벤트 바인딩
        GetButton((int)Buttons.Start).gameObject.AddUIEvent(OnClickStart);
        GetButton((int)Buttons.Settings).gameObject.AddUIEvent(OnClickSettings);
        GetButton((int)Buttons.Exit).gameObject.AddUIEvent(OnClickExit);
    }

    void OnClickStart(PointerEventData data)
    {
        Debug.Log("Start");
        // 매니저.씬.로드씬(셀렉트스테이지)
    }

    void OnClickSettings(PointerEventData data)
    {
        Debug.Log("Settings");
        // 세팅 팝업 켜기
    }

    void OnClickExit(PointerEventData data)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
