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
        if (Input.GetKeyDown(KeyCode.M))
        {
            int userChoose = ChinarMessage.ShowMsg("Unlaugh 未响应\n是否关闭该程序？", "Unlaugh");
            print(userChoose);
        }
    }
    # endif
}
