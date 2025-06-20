using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } =  Define.Scene.Unknown;

    void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        Object obj = FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
    
    public abstract void Clear();
}
