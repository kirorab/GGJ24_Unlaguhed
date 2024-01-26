using UnityEngine;
using UnityEngine.EventSystems; // 需要引入事件命名空间

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 scaleOnHover = new Vector3(1.1f, 1.1f, 1.1f); // 悬停时的放大比例
    private Vector3 originalScale; // 原始大小
    private void Start()
    {
        originalScale = transform.localScale; // 保存原始大小
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = scaleOnHover; // 鼠标悬停时放大
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // 鼠标离开时恢复原始大小
    }
}