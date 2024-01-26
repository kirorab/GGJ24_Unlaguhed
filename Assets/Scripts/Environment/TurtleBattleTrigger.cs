using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventSystem.Instance.Invoke(EEvent.OnStartTurtleBattle);
        GetComponent<Collider2D>().enabled = false;
    }
}
