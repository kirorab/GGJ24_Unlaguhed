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
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, LockCameraOnPokemonBattle);
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, RestoreCamera);
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
        transposer.m_XDamping = xDamping;
        virCam.Follow = pokemonBattleCenter;
    }

    private void RestoreCamera()
    {
        virCam.Follow = PlayerInfo.Instance.transform;
    }

    public void RemoveXDamping()
    {
        dampingCoroutine = StartCoroutine(DoRemoveXDamping());
    }

    private IEnumerator DoRemoveXDamping()
    {
        float duration = 1f;
        float time = 0;
        while (time < duration)
        {
            transposer.m_XDamping = xDamping * (1 - time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transposer.m_XDamping = 0;
    }
}
