using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : Singleton<UIManager>
{
    public GameObject SaveFailed;
    public GameObject DandCbg;
    public GameObject TurtleChoose;
    public VideoPlayer videoPlayer;

    protected override void Awake()
    {
        base.Awake();
        SaveFailed.SetActive(false);
        DandCbg.SetActive(false);
        TurtleChoose.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
        EventSystem.Instance.AddListener(EEvent.OnSaveFailed, HandleSaveFailed);
        EventSystem.Instance.AddListener(EEvent.OnStartDialogue, SetDandCbgActiveTrue);
        EventSystem.Instance.AddListener(EEvent.OnEndDialogue, SetDandCbgActiveFalse);
        EventSystem.Instance.AddListener(EEvent.OnTurtleChoose, HandleTurtleChoose);
        EventSystem.Instance.AddListener(EEvent.OnTurtleChoose, SetDandCbgActiveTrue);
        videoPlayer.loopPointReached += OnVideoEnd;
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
        TurtleChoose.SetActive(true);
    }
    
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SaveFailed.SetActive(false);
    }

    public void ForgiveTurtle(bool isForgive)
    {
        EventSystem.Instance.Invoke<bool>(EEvent.OnEndTurtleChoose, isForgive);
        TurtleChoose.SetActive(false);
        SetDandCbgActiveFalse();
    }

    private void SetDandCbgActiveTrue()
    {
        DandCbg.SetActive(true);
    }

    private void SetDandCbgActiveFalse()
    {
        DandCbg.SetActive(false);
    }

    public void PlayVideo()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer _)
    {
        videoPlayer.gameObject.SetActive(false);
        EventSystem.Instance.Invoke(EEvent.OnStartPokemonBattle);
    }
}
