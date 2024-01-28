using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWallController : MonoBehaviour
{
    public Collider2D[] walls;
    public Collider2D[] triggers;
    public PolygonCollider2D cameraCollider;

    private void Awake()
    {
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndLaughChoose, UpdateColliders);
    }

    private void UpdateColliders(bool laugh)
    {
        if (!laugh)
        {
            foreach (var collider in walls)
            {
                collider.enabled = false;
            }
            foreach (var trigger in triggers)
            {
                trigger.enabled = true;
            }
            cameraCollider.points = new Vector2[]
            {
                new Vector2(-20, cameraCollider.points[0].y),
                new Vector2(180, cameraCollider.points[1].y),
                new Vector2(180, cameraCollider.points[2].y),
                new Vector2(-20, cameraCollider.points[3].y)
            };
            Camera.main.GetComponentInChildren<CinemachineConfiner2D>().InvalidateCache();
        }
    }
}

