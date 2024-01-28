using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBoard : InteractiveObject
{
    private void Awake()
    {
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndLaughChoose, EnableInteract);
    }

    private void EnableInteract(bool laugh)
    {
        if (!laugh)
        {
            isInteracted = false;
        }
    }

    public override void OnInteract()
    {
        EventSystem.Instance.Invoke(EEvent.OnLaughChoose);
    }

    public override void Update()
    {
        BaseUpdate();
    }
}
