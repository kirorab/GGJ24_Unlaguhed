using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject detail; // 按钮详情文本UI
    public string detailString = "这是按钮的详情"; // 显示的详情内容

    private void Awake()
    {
        detail.SetActive(false);
        detail.GetComponentInChildren<TMP_Text>().text = detailString;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detail.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detail.SetActive(false);
    }
}
