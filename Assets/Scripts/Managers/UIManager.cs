using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    UI_Scene _scene = null;
    
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject("@UI_Root");
            return root;
        }
    }

    /// <summary>
    /// Canvas 설정
    /// </summary>
    /// <param name="go"></param>
    /// <param name="sort"></param>
    public void SetCanvas(GameObject go, bool sort = false)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; // 부모의 Sort 무시

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    
    /// <summary>
    /// SceneUI 켜기
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public UI_Scene ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go =  Managers.Resource.Instantiate($"UI/Scene/{name}", Root.transform);
        UI_Scene scene = Util.GetOrAddComponent<UI_Scene>(go);
        
        _scene = scene;
        
        return scene;
    }
    
    /// <summary>
    /// PopupUI 켜기
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public UI_Popup ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go =  Managers.Resource.Instantiate($"UI/Popup/{name}", Root.transform);
        UI_Popup popup = Util.GetOrAddComponent<UI_Popup>(go);
        
        _popupStack.Push(popup);
        
        return popup;
    }
    
    /// <summary>
    /// 특정 PopupUI 닫기 : 맨 위에 있지 않다면 닫히지 않음
    /// </summary>
    /// <param name="popup"></param>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }
    
    /// <summary>
    /// PopupUI 닫기
    /// </summary>
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;
        
        UI_Popup popup = _popupStack.Pop();
        Object.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }
}
