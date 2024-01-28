using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpasmController : CharacterBugController
{
    public float spasmDuration = 0.1f;
    public float spasmIntensity = 0.5f;

    private void Start()
    {
        StartBugging();
    }

    public override void StartBugging()
    {
        StartCoroutine(SpasmCoroutine());
    }

    private IEnumerator SpasmCoroutine()
    {
        Vector3 originalPosition = transform.position;
        while (true)
        {
            // �������ƫ��ֵ
            Vector3 spasmOffset = new Vector3(Random.Range(-spasmIntensity, spasmIntensity), 
                Random.Range(-spasmIntensity, spasmIntensity));

            // Ӧ��ƫ��ֵ
            transform.position = originalPosition + spasmOffset;

            // �ȴ�һ��ʱ��
            yield return new WaitForSeconds(spasmDuration);
        }
    }
}
