using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject SaveFailed;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnSaveFailed, HandleSaveFailed);
        EventSystem.Instance.AddListener(EEvent.OnTurtleChoose, HandleTurtleChoose);
    }

    // Start is called before the first frame update
    public void HandleSaveFailed()
    {
        Debug.Log("save failed");
        SaveFailed.SetActive(true);
        StartCoroutine(Wait(2));
        
    }

    public void HandleTurtleChoose()
    {
        
    }
    
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SaveFailed.SetActive(false);
    }

    private void HandleMsgBoxWait(int index)
    {
        
    }
}
