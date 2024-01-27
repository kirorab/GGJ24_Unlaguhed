using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    // ��ȡ����ʵ��
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
                singleton.name = "Singleton_" + typeof(T).ToString();

                DontDestroyOnLoad(singleton);
            }

            return _instance;
        }
    }
}

