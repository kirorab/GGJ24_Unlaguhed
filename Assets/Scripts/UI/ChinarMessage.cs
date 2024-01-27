using System;
using System.Runtime.InteropServices;//调用外部库，需要引用命名空间


/// <summary>
/// 为了调用外部库脚本
/// </summary>
public static class ChinarMessage
{
    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr handle, String message, String title, int type);//具体方法

    public static void ShowMsg(string msg, string title)
    {
        MessageBox(IntPtr.Zero, msg, title, 1);
    }
}
