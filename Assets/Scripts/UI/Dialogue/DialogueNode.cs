using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
[System.Serializable]
public class DialogueNode 
{
    public string nodeName;
    public string speaker;
    [TextArea(3,10)][Multiline]
    public string content;
    public int nextIndex;
}