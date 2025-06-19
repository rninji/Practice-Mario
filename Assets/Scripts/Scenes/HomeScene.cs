using UnityEngine;

/// <summary>
/// 홈 화면 Scene
/// </summary>
public class HomeScene : BaseScene
{
    public override void Init()
    {
        SceneType =  Define.Scene.Home;

        // UI 표시
        Managers.UI.ShowSceneUI<UI_Home>();
    }

    public override void Clear()
    {
    }
} 
