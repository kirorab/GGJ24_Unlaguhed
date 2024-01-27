using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bonfire : InteractiveObject
{
    public Sprite fire;
    private Sprite _extinct;
    private SpriteRenderer _sprite;
    private Transform _transform;
    [SerializeField] private float blockLength = 2.25f;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _extinct = _sprite.sprite;
    }

    public override void OnInteract()
    {
        //Debug.Log("interact");
        _sprite.sprite = fire;
        var vector3 = transform.position;
        vector3.y += 1;
        transform.position = vector3;
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        isInteracted = true;
    }

    private void Update()
    {
        BaseUpdate();
        if (isInteracted)
        {
            if (PlayerInfo.Instance.GetPlayerPosition().position.x - _transform.position.x > 2 * blockLength)
            {
                Extinct();
            }
        }
    }

    private void Extinct()
    {
        _sprite.sprite = _extinct;
        isInteracted = false;
        EventSystem.Instance.Invoke(EEvent.OnSaveFailed);
    }
}
