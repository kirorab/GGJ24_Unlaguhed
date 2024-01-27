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
    protected virtual void Awake()
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
        vector3.y += 0.5f;
        transform.position = vector3;
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        isInteracted = true;
    }

    public override void Update()
    {
        BaseUpdate();
        if (isInteracted)
        {
            if (Math.Abs(PlayerInfo.Instance.GetPlayerPosition().position.x - _transform.position.x) > 2 * blockLength)
            {
                Extinct();
            }
        }
    }

    private void Extinct()
    {
        _sprite.sprite = _extinct;
        isInteracted = false;
        var vector3 = transform.position;
        vector3.y -= 0.5f;
        transform.position = vector3;
        transform.localScale = new Vector3(1/0.75f, 1/0.75f, 1);
        EventSystem.Instance.Invoke(EEvent.OnSaveFailed);
    }
}
