using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotateController : CharacterBugController
{
    public float rotateSpeed = 500f;

    private void Start()
    {
        StartBugging();
    }

    public override void StartBugging()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            // 应用偏移值
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 
                transform.localEulerAngles.y, transform.localEulerAngles.z + rotateSpeed * Time.deltaTime);

            // 等待一段时间
            yield return null;
        }
    }
}
