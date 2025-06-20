using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    public string GetSceneName(Define.Scene type)
    {
        return Enum.GetName(typeof(Define.Scene), type);
    }
    
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
