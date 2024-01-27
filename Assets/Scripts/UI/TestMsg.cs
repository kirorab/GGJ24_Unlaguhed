using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMsg : MonoBehaviour
{
    // Start is called before the first frame update
    public void ShowMsg()
    {
        ChinarMessage.MessageBox(IntPtr.Zero, "Chinar-1:确认：1，取消：2", "确认|取消", 1);
    }
}
