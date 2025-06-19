using UnityEngine;

public class UIManager 
{
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
    
    // SceneUI 켜기
    public UI_Scene ShowSceneUI<T>(string name = null)
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go =  Managers.Resource.Instantiate($"UI/Scene/{name}", Root.transform);
        UI_Scene scene = Util.GetOrAddComponent<UI_Scene>(go);
        return scene;
    }
}
