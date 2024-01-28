using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlyController : CharacterBugController
{
    public float flySpeed = 3f;

    private void Start()
    {
        StartBugging();
    }

    public override void StartBugging()
    {
        StartCoroutine(FlyCoroutine());
    }

    private IEnumerator FlyCoroutine()
    {
        while (true)
        {
            // 应用偏移值
            transform.Translate(0, flySpeed * Time.deltaTime, 0, Space.World);

            // 等待一段时间
            yield return null;
        }
    }
}
