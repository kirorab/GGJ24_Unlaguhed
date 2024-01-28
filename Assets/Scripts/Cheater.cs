using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    public Turtle turtle;

    # if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            turtle.Cheat();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventSystem.Instance.Invoke(EEvent.OnEndPokemonBattle);
        }
    }
    # endif
}
