using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool laughed = false;

    private static GameManager _instance;

    // 获取单例实例
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<GameManager>();
                singleton.name = "Singleton_" + typeof(GameManager).ToString();
                DontDestroyOnLoad(singleton);
            }

            return _instance;
        }
    }
}
