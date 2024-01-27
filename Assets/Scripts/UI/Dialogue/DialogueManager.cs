using System;
using System.Collections;
using TarodevController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public float delay = 0.1f;

    public GameObject dialogueBox;
    public TMP_Text _Text;
    public GameObject continueMark;
    private bool isTyping;
    private string currentText;
    private int dialogueIndexNow = 0;
    private Dialogues _dias;
    private void Awake()
    {
        dialogueBox.SetActive(true);
        EventSystem.Instance.AddListener<Dialogues>(EEvent.OnstartDialogueWithDialogues, HandleDialogue);
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener<Dialogues>(EEvent.OnstartDialogueWithDialogues, HandleDialogue);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void Update()
    {
        if (!dialogueBox.activeSelf)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0)) // 检测鼠标点击
        {
            if (isTyping)
            {
                CompleteText(); // 如果正在打字，则补全文本
                continueMark.SetActive(true);
            }
            else
            {
                ShowNextText(); // 显示下一段文本
            }
        }
    }
    private void HandleDialogue(Dialogues dias)
    {
        dialogueIndexNow = 0;
        dialogueBox.SetActive(true);
        _dias = dias;
        StartTyping(_dias.dialogueList[dialogueIndexNow]);
    }
    
    IEnumerator ShowText(string fullText)
    {
        for (int i = 0; i < fullText.Length; i++)//遍历插入字符串的长度
        {
            var currentText = fullText.Substring(0, i);
            _Text.text = currentText;
            yield return new WaitForSeconds(delay);//每次延迟的时间 数值越小 延迟越少
        }
    }
    
    public void StartTyping(DialogueNode diaNode)
    {
        currentText = diaNode.content;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        _Text.text = "";
        foreach (char c in currentText)
        {
            if (!isTyping)
            {
                break;
            }
            _Text.text += c;
            yield return new WaitForSeconds(delay); // 控制打字速度
        }
        isTyping = false;
        continueMark.SetActive(true);
    }

    private void CompleteText()
    {
        StopCoroutine(TypeText()); // 停止当前打字协程
        _Text.text = currentText; // 直接显示完整文本
        isTyping = false;
    }

    private void ShowNextText()
    {
        continueMark.SetActive(false);
        dialogueIndexNow = _dias.dialogueList[dialogueIndexNow].nextIndex;
        if (dialogueIndexNow == -1)
        {
            dialogueBox.SetActive(false);
            EventSystem.Instance.Invoke(EEvent.OnEndDialogue);
            return;
        }
        StartTyping(_dias.dialogueList[dialogueIndexNow]);
    }
}