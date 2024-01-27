using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    # if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventSystem.Instance.Invoke(EEvent.OnEndTurtleBattle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventSystem.Instance.Invoke(EEvent.OnStartPokemonBattle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventSystem.Instance.Invoke(EEvent.OnEndPokemonBattle);
        }
    }
    # endif
}
