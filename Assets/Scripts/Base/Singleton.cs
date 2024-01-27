using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    // 获取单例实例
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
                singleton.name = "Singleton_" + typeof(T).ToString();
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this as T;
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, () => { _instance = null; });
    }
}

