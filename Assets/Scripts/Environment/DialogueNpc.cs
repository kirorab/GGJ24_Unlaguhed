using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpc : InteractiveObject
{
    public Dialogues dias;
    // Start is called before the first frame update

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnStartTurtleBattle, () => isInteracted = true);
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
}
