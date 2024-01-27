using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpc : InteractiveObject
{
    public Dialogues dias;
    // Start is called before the first frame update
    public override void OnInteract()
    {
        EventSystem.Instance.Invoke<Dialogues>(EEvent.OnStartDialogue, dias);
        Debug.Log("interact");
    }

    public override void Update()
    {
        BaseUpdate();
    }
}
