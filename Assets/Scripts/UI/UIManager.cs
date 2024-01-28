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
    public GameObject laughChoose;
    public VideoPlayer whoIAm;
    public VideoPlayer closingCredits;
    public Sprite muteSprite;
    public Sprite unMuteSprite;
    public Image muteImage;

    protected override void Awake()
    {
        base.Awake();
        SaveFailed.SetActive(false);
        DandCbg.SetActive(false);
        TurtleChoose.SetActive(false);
        whoIAm.gameObject.SetActive(false);
        closingCredits.gameObject.SetActive(false);
        EventSystem.Instance.AddListener(EEvent.OnSaveFailed, HandleSaveFailed);
        EventSystem.Instance.AddListener(EEvent.OnStartDialogue, SetDandCbgActiveTrue);
        EventSystem.Instance.AddListener(EEvent.OnEndDialogue, SetDandCbgActiveFalse);
        EventSystem.Instance.AddListener(EEvent.OnTurtleChoose, HandleTurtleChoose);
        whoIAm.loopPointReached += OnWhoIAmVideoEnd;
        EventSystem.Instance.AddListener(EEvent.OnLaughChoose, HandleLaughChoose);
        closingCredits.loopPointReached += (VideoPlayer _) => ReturnToMenuScene();
        EventSystem.Instance.AddListener<bool>(EEvent.OnToggleMute, OnToggleMute);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (whoIAm.isPlaying)
            {
                whoIAm.time = whoIAm.clip.length;
            }
            if (closingCredits.isPlaying)
            {
                closingCredits.time = closingCredits.clip.length;
            }
        }
    }
#endif

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
        SetDandCbgActiveTrue();
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

    public void PlayWhoIAmVideo()
    {
        whoIAm.gameObject.SetActive(true);
        whoIAm.Play();
    }

    private void OnWhoIAmVideoEnd(VideoPlayer _)
    {
        whoIAm.gameObject.SetActive(false);
        EventSystem.Instance.Invoke(EEvent.OnStartPokemonBattle);
    }

    public void HandleLaughChoose()
    {
        laughChoose.SetActive(true);
        SetDandCbgActiveTrue();
    }

    public void OnEndLaughChoose(bool laugh)
    {
        EventSystem.Instance.Invoke<bool>(EEvent.OnEndLaughChoose, laugh);
        laughChoose.SetActive(false);
        SetDandCbgActiveFalse();
        if (laugh)
        {
            PlayClosingCreditsVideo();
            GameManager.Instance.laughed = true;
        }
        else
        {
            PlayerInfo.Instance.transform.position = new Vector3(-4.5f, -1.89f, 0);
        }
    }

    private void PlayClosingCreditsVideo()
    {
        closingCredits.gameObject.SetActive(true);
        closingCredits.Play();
    }

    public void ReturnToMenuScene()
    {
        SceneSystem.Instance.LoadScene(EScene.MenuScene);
    }

    public void ToggleMute()
    {
        AudioManager.Instance.ToggleMute();
    }

    private void OnToggleMute(bool mute)
    {
        if (mute)
        {
            muteImage.sprite = muteSprite;
        }
        else
        {
            muteImage.sprite = unMuteSprite;
        }
        whoIAm.SetDirectAudioMute(0, mute);
        closingCredits.SetDirectAudioMute(0, mute);
    }
}
