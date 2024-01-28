using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpc : InteractiveObject
{
    public Dialogues dias;
    public GameObject reminder;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnLaughChoose, AfterChooseLaugh);
    }

    public override void OnInteract()
    {
        EventSystem.Instance.Invoke(EEvent.OnStartDialogue);
        EventSystem.Instance.Invoke<Dialogues>(EEvent.OnstartDialogueWithDialogues, dias);
        //Debug.Log("interact");
    }

    public override void Update()
    {
        BaseUpdate();
    }

    private void AfterChooseLaugh()
    {
        isInteracted = true;
        reminder.SetActive(false);
    }
}
