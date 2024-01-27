
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogues", menuName = "Dialogue System/Dialogues")]
public class Dialogues : ScriptableObject
{
    
    public List<DialogueNode> dialogueList = new List<DialogueNode>();
}
