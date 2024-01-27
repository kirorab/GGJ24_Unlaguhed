using System;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public GameObject hint;
    protected bool isPlayerNearBy;
    protected bool isInteracted;
    public abstract void OnInteract();

    private void Start()
    {
        hint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInteracted)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearBy = true;
            hint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearBy = false;
            hint.SetActive(false);
        }
    }

    protected void BaseUpdate()
    {
        if (isPlayerNearBy && !isInteracted && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("interact");
            hint.SetActive(false);
            OnInteract();
            isInteracted = true;
        }
        
    }
}
