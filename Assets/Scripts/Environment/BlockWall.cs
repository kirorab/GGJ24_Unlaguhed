using System.Collections;
using UnityEngine;

public class BlockWall : MonoBehaviour
{
    private void Awake()
    {
        EventSystem.Instance.AddListener(EEvent.OnEndTurtleBattle, RemoveBlockWall);
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void RemoveBlockWall()
    {
        StartCoroutine(DoMove());
    }

    private IEnumerator DoMove()
    {
        float time = 0;
        float duration = 1f;
        float endPosY = 4.3f;
        while (time < duration)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, endPosY * time / duration, 0);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener(EEvent.OnEndTurtleBattle, RemoveBlockWall);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }
}
