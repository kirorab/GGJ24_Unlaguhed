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
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DoChangeXDamping());
    }

    private IEnumerator DoChangeXDamping()
    {
        CinemachineVirtualCamera virCam = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        CinemachineTransposer transposer = virCam.GetCinemachineComponent<CinemachineTransposer>();
        float duration = 1f;
        float time = 0;
        float start = transposer.m_XDamping;
        while (time < duration)
        {
            transposer.m_XDamping = start * (1 - time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transposer.m_XDamping = 0;
    }
}
