using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;



public class MessageBox : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int MBox(IntPtr hWnd, String text, String caption, uint type);

    // 定义MessageBox的一些标准参数
    private const uint MB_OKCANCEL = 0x00000001;
    private const uint MB_ICONQUESTION = 0x00000020;
    
    public void ShowMessageBox()
    {
        int result = MBox(IntPtr.Zero, "你的消息内容", "消息框标题", MB_OKCANCEL | MB_ICONQUESTION);

        if (result == 1) // "OK"被点击
        {
            // 处理“确定”逻辑
            Debug.Log("确定被点击");
        }
        else if (result == 2) // "Cancel"被点击
        {
            // 处理“取消”逻辑
            Debug.Log("取消被点击");
        }
    }

}
