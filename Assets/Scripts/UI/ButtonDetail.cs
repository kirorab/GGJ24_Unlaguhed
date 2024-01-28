using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject detail; // 按钮详情文本UI

    private void Awake()
    {
        detail.SetActive(false);
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
