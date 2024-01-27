using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire3 : Bonfire
{
    protected override void Awake()
    {
        base.Awake();
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, ShowSelf);
    }

    private void ShowSelf()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();
        PlayerInfo.Instance.TakeDamage(1);
    }

    public override void Update()
    {
        BaseUpdate();
    }
}
