using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : InteractiveObject
{
    public override void OnInteract()
    {
        EventSystem.Instance.Invoke(EEvent.OnTriggerPokemonBattle);
    }

    public override void Update()
    {
        BaseUpdate();
    }
}
