using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMsg : MonoBehaviour
{
    // Start is called before the first frame update
    public void ShowMsg()
    {
        ChinarMessage.ShowMsg("Hello World!", "Test");
    }
}
