using System;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public GameObject hint;
    private bool isPlayerNearBy;
    private bool isInteracted;
    public abstract void OnInteract();

    private void Start()
    {
        hint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
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

    private void Update()
    {
        if (isPlayerNearBy && !isInteracted && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
            isInteracted = true;
        }
        
    }
}
