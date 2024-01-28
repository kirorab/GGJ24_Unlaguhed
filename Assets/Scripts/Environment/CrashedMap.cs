using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashedMap : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndLaughChoose, ShowCrashedMap);
    }

    private void ShowCrashedMap(bool laugh)
    {
        if (!laugh)
        {
            gameObject.SetActive(true);
        }
    }
}
