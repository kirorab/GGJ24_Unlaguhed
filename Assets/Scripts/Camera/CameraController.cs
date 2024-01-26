using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virCam;

    public Transform turtleBattleCenter;
    public Transform pokemonBattleCenter;
    public Transform playerTransform;

    private void Awake()
    {
        virCam = GetComponentInChildren<CinemachineVirtualCamera>();
        EventSystem.Instance.AddListener(EEvent.OnStartTurtleBattle, LockCameraOnTurtleBattle);
        EventSystem.Instance.AddListener(EEvent.OnEndTurtleBattle, RestoreCamera);
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, LockCameraOnPokemonBattle);
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, RestoreCamera);
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    private void LockCameraOnTurtleBattle()
    {
        virCam.Follow = turtleBattleCenter;
    }

    private void LockCameraOnPokemonBattle()
    {
        virCam.Follow = pokemonBattleCenter;
    }

    private void RestoreCamera()
    {
        // TODO Íæ¼Ò¾µÍ·ÇÐ»»
        virCam.Follow = playerTransform;
    }

    private void BeforeLoadScene()
    {
        EventSystem.Instance.RemoveListener(EEvent.OnStartTurtleBattle, LockCameraOnTurtleBattle);
        EventSystem.Instance.RemoveListener(EEvent.OnEndTurtleBattle, RestoreCamera);
        EventSystem.Instance.RemoveListener(EEvent.OnStartPokemonBattle, LockCameraOnPokemonBattle);
        EventSystem.Instance.RemoveListener(EEvent.OnEndPokemonBattle, RestoreCamera);
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }
}
