using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virCam;
    private CinemachineTransposer transposer;
    private Coroutine dampingCoroutine;
    private float xDamping;

    public Transform turtleBattleCenter;
    public Transform pokemonBattleCenter;

    private void Awake()
    {
        virCam = GetComponentInChildren<CinemachineVirtualCamera>();
        transposer = virCam.GetCinemachineComponent<CinemachineTransposer>();
        xDamping = transposer.m_XDamping;
        EventSystem.Instance.AddListener(EEvent.OnStartTurtleBattle, LockCameraOnTurtleBattle);
        EventSystem.Instance.AddListener(EEvent.OnEndTurtleBattle, RestoreCamera);
        EventSystem.Instance.AddListener(EEvent.OnTriggerPokemonBattle, LockCameraOnPokemonBattle);
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, RestoreCamera);
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndLaughChoose, (bool laugh) => { if (!laugh) SetXDamping(0); });
    }

    private void LockCameraOnTurtleBattle()
    {
        virCam.Follow = turtleBattleCenter;
    }

    private void LockCameraOnPokemonBattle()
    {
        if (dampingCoroutine != null)
        {
            StopCoroutine(dampingCoroutine);
            dampingCoroutine = null;
        }
        SetXDamping(xDamping);
        virCam.Follow = pokemonBattleCenter;
    }

    private void RestoreCamera()
    {
        virCam.Follow = PlayerInfo.Instance.transform;
    }

    public void RemoveXDampingSlowly()
    {
        dampingCoroutine = StartCoroutine(DoRemoveXDamping());
    }

    private IEnumerator DoRemoveXDamping()
    {
        float duration = 1f;
        float time = 0;
        while (time < duration)
        {
            SetXDamping(xDamping * (1 - time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        SetXDamping(0);
    }

    private void SetXDamping(float xDamping)
    {
        transposer.m_XDamping = xDamping;
    }
}
