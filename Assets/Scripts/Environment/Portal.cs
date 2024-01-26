using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool right;
    public Transform destination;
    public Transform player;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, RemovePortalTrigger);
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void RemovePortalTrigger()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener(EEvent.OnStartPokemonBattle, RemovePortalTrigger);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // TODO 玩家瞬移
        if ((right && player.position.x > transform.position.x) || (!right && player.position.x < transform.position.x))
        {
            float delta = player.position.x - transform.position.x;
            Transform cameraTransform = Camera.main.transform;
            float offset = cameraTransform.position.x - player.transform.position.x;
            player.transform.position = new Vector3(destination.position.x + delta, player.transform.position.y, 0);
            cameraTransform.GetComponentInChildren<CinemachineVirtualCamera>().ForceCameraPosition(player.transform.position + offset * Vector3.right, Quaternion.identity);
        }
    }
}
