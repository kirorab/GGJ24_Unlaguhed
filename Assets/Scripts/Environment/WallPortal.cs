using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPortal : MonoBehaviour
{
    private bool crashed;
    public Vector3 destination;

    private void Awake()
    {
        crashed = false;
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndLaughChoose, ChangePortalState);
    }

    private void ChangePortalState(bool laugh)
    {
        if (!laugh)
        {
            crashed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (crashed)
        {
            PlayerInfo.Instance.transform.position = destination;
        }
    }
}
