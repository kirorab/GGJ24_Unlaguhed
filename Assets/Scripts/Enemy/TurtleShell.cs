using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETurtleShellState
{
    Rotating,
    Moving,
}

public class TurtleShell : MonoBehaviour
{
    public float rotateRadius;
    public float rotateSpeed;
    public float moveSpeed;
    private Vector3 moveDir;

    private ETurtleShellState currentState;

    private float angle;
    public float Angle
    {
        get => angle;
        set
        {
            angle = value;
            transform.localPosition = new Vector3(rotateRadius * Mathf.Cos(angle * Mathf.Deg2Rad), 
                rotateRadius * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        }
    }

    private void Awake()
    {
        currentState = ETurtleShellState.Rotating;
    }

    private void Update()
    {
        if (currentState == ETurtleShellState.Rotating)
        {
            Angle += rotateSpeed * Time.deltaTime;
        }
        else if (currentState == ETurtleShellState.Moving)
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveDir);
            if (transform.position.x < 26f ||  transform.position.x > 47f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SwitchStateThrowingToPlayer()
    {
        transform.parent = null;
        currentState = ETurtleShellState.Moving;
        moveDir = (PlayerInfo.Instance.transform.position + Vector3.up * 1.5f - transform.position).normalized;
    }
}
