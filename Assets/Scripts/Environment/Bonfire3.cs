using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire3 : MonoBehaviour, IInteractive
{
    private void Awake()
    {
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

    public void OnInteract()
    {

    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener(EEvent.OnEndPokemonBattle, ShowSelf);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }
}
