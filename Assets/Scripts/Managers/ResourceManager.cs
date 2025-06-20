using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Resources.Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }
}
