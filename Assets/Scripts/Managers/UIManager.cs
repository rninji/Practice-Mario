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

