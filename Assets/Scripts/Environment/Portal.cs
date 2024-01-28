using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool right;
    public Transform destination;
    public Turtle turtle;
    private Transform player;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnTriggerPokemonBattle, RemovePortalTrigger);
        player = PlayerInfo.Instance.GetPlayerPosition();
    }

    private void RemovePortalTrigger()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((right && player.position.x > transform.position.x) || (!right && player.position.x < transform.position.x))
            {
                float delta = player.position.x - transform.position.x;
                float turtleDelta = 0;
                if (turtle != null && turtle.TurtleState == ETurtleState.Follow)
                {
                    turtleDelta = turtle.transform.position.x - player.position.x;
                }
                player.transform.position = new Vector3(destination.position.x + delta, player.transform.position.y, 0);
                
                Transform cameraTransform = Camera.main.transform;
                float offset = cameraTransform.position.x - player.transform.position.x;
                cameraTransform.GetComponentInChildren<CinemachineVirtualCamera>().ForceCameraPosition(player.transform.position + offset * Vector3.right, Quaternion.identity);
                
                if (turtle != null && turtle.TurtleState == ETurtleState.Follow)
                {
                    turtle.transform.position = new Vector3(player.transform.position.x + turtleDelta, turtle.transform.position.y, 0);
                }
            }
        }
    }
}
