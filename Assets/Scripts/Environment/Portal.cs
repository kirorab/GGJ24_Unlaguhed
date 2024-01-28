using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool right;
    public Transform destination;
    public Turtle turtle;

    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnTriggerPokemonBattle, RemovePortalTrigger);
    }

    private void RemovePortalTrigger()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((right && PlayerInfo.Instance.GetPlayerPosition().position.x > transform.position.x) || 
                (!right && PlayerInfo.Instance.GetPlayerPosition().position.x < transform.position.x))
            {
                float delta = PlayerInfo.Instance.GetPlayerPosition().position.x - transform.position.x;
                float turtleDelta = 0;
                if (turtle != null && turtle.TurtleState == ETurtleState.Follow)
                {
                    turtleDelta = turtle.transform.position.x - PlayerInfo.Instance.GetPlayerPosition().position.x;
                }
                PlayerInfo.Instance.GetPlayerPosition().position = new Vector3(destination.position.x + delta, PlayerInfo.Instance.GetPlayerPosition().position.y, 0);
                
                if (turtle != null && turtle.TurtleState == ETurtleState.Follow)
                {
                    turtle.transform.position = new Vector3(PlayerInfo.Instance.GetPlayerPosition().position.x + turtleDelta, turtle.transform.position.y, 0);
                }
            }
        }
    }
}
