using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire3 : Bonfire
{
    private void Awake()
    {
        base.Awake();
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, ShowSelf);
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
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
    private void Update()
    {
        BaseUpdate();
        
    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener(EEvent.OnEndPokemonBattle, ShowSelf);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }
}
