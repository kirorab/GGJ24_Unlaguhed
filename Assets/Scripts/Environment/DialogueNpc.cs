using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpc : InteractiveObject
{
    public Dialogues beginDias;
    public Dialogues endDias;
    private Dialogues curDias;
    public GameObject reminder;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnLaughChoose, AfterChooseLaugh);
        curDias = beginDias;
    }

    public override void OnInteract()
    {
        EventSystem.Instance.Invoke(EEvent.OnStartDialogue);
        EventSystem.Instance.Invoke<Dialogues>(EEvent.OnstartDialogueWithDialogues, curDias);
        //Debug.Log("interact");
    }

    public override void Update()
    {
        BaseUpdate();
    }

    private void AfterChooseLaugh()
    {
        isInteracted = false;
        reminder.SetActive(false);
        curDias = endDias;
    }
}
