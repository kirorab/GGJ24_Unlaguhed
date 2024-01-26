using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPoundingTheWallTrigger : MonoBehaviour
{
    public Transform[] rendererTransforms;
    public Collider2D[] colliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tr in rendererTransforms)
        {
            foreach (var r in tr.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
        }
        foreach (var col in colliders)
        {
            col.enabled = true;
        }
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().
        GetComponent<Collider2D>().enabled = false;
    }
}
