using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : InteractiveObject
{
    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, (() => gameObject.SetActive(false)));
    }

    public override void OnInteract()
    {
        EventSystem.Instance.Invoke(EEvent.OnTriggerPokemonBattle);
    }

    public override void Update()
    {
        BaseUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Apple")
        {
            EventSystem.Instance.Invoke(EEvent.OnAppleHitPikachu);
            Destroy(other.gameObject);
        }
    }
}
