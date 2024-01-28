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
            // Ӧ��ƫ��ֵ
            transform.Translate(0, flySpeed * Time.deltaTime, 0, Space.World);

            // �ȴ�һ��ʱ��
            yield return null;
        }
    }
}
