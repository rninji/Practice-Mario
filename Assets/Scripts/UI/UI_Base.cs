using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();
    
    public abstract void Init();
    
    void Start()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : Object
    {
        string[] names = Enum.GetNames(type);
        
        Object[] objects = new Object[names.Length];
        _objects.Add(type, objects);

        for(int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);    
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            
            if (objects[i] == null)
                Debug.Log($"Fail to bind : {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : Object
    {
        Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;
        
        return objects[idx] as T;
    }
    
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }

    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    
    protected Image GetImage(int idx) { return Get<Image>(idx); }
