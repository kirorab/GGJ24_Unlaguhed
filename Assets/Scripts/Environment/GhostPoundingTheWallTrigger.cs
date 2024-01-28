using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPoundingTheWallTrigger : MonoBehaviour
{
    public Transform[] rendererTransforms;
    public Collider2D[] colliders;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnTriggerPokemonBattle, RestoreRenderers);
    }

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
        Camera.main.GetComponent<CameraController>().RemoveXDampingSlowly();
    }

    private void RestoreRenderers()
    {
        foreach (var tr in rendererTransforms)
        {
            foreach (var r in tr.GetComponentsInChildren<Renderer>())
            {
                r.enabled = true;
            }
        }
    }
}
