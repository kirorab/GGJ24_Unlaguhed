using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public enum BGMType
{
    Normal,
    Turtle,
    TurtleWin,
    Pokemon,
    PokemonWin,
}

public enum AudioType
{
    Jump,
    KickOrImpact,
    ElectricShock,
    PikachuShout,
}

public class AudioManager : Singleton<AudioManager>
{
    public bool muted;
    public AudioClip normalClip;
    public AudioClip turtleClip;
    public AudioClip turtleWinClip;
    public AudioClip pokemonClip;
    public AudioClip pokemonWinClip;
    public AudioClip jumpClip;
    public AudioClip kickOrImpactClip;
    public AudioClip electricShockClip;
    public AudioClip pikachuShoutClip;

    private Dictionary<BGMType, AudioClip> bgms;
    private AudioSource bgmAudioSource;
    private Dictionary<AudioType, AudioSource> audioSources;

    protected override void Awake()
    {
        base.Awake();
        bgms = new Dictionary<BGMType, AudioClip>
        {
            [BGMType.Normal] = normalClip,
            [BGMType.Turtle] = turtleClip,
            [BGMType.TurtleWin] = turtleWinClip,
            [BGMType.Pokemon] = pokemonClip,
            [BGMType.PokemonWin] = pokemonWinClip
        };
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        PlayBgm(BGMType.Normal);
        EventSystem.Instance.AddListener(EEvent.OnStartTurtleBattle, () => PlayBgm(BGMType.Turtle));
        EventSystem.Instance.AddListener(EEvent.OnEndTurtleBattle, () => PlayBgm(BGMType.TurtleWin, true));

        EventSystem.Instance.AddListener(EEvent.OnTriggerPokemonBattle, () => bgmAudioSource.Pause());
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, () => PlayBgm(BGMType.Pokemon));
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, () => PlayBgm(BGMType.PokemonWin, true));
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndLaughChoose, (bool laugh) => { if (laugh) bgmAudioSource.Pause(); });

        audioSources = new Dictionary<AudioType, AudioSource>();
        AudioSource jumpAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource.clip = jumpClip;
        jumpAudioSource.loop = false;
        audioSources[AudioType.Jump] = jumpAudioSource;
        AudioSource KickOrImpactAudioSource = gameObject.AddComponent<AudioSource>();
        KickOrImpactAudioSource.clip = kickOrImpactClip;
        KickOrImpactAudioSource.loop = false;
        audioSources[AudioType.KickOrImpact] = KickOrImpactAudioSource;
        AudioSource electricShockAudioSource = gameObject.AddComponent<AudioSource>();
        electricShockAudioSource.clip = electricShockClip;
        electricShockAudioSource.loop = false;
        audioSources[AudioType.ElectricShock] = electricShockAudioSource;
        AudioSource pikachuShoutAudioSource = gameObject.AddComponent<AudioSource>();
        pikachuShoutAudioSource.clip = pikachuShoutClip;
        pikachuShoutAudioSource.loop = false;
        audioSources[AudioType.PikachuShout] = pikachuShoutAudioSource;

        EventSystem.Instance.AddListener(EEvent.OnStartTurtleBattle, () => PlayerInfo.Instance.GetComponent<PlayerController>().Jumped += PlayJumpAudio);
        EventSystem.Instance.AddListener(EEvent.OnEndTurtleBattle, () => PlayerInfo.Instance.GetComponent<PlayerController>().Jumped -= PlayJumpAudio);
    }

    public void ToggleMute()
    {
        muted = !muted;
        EventSystem.Instance.Invoke<bool>(EEvent.OnToggleMute, muted);
        bgmAudioSource.mute = muted;
        foreach (AudioSource source in audioSources.Values)
        {
            source.mute = muted;
        }
    }

    public void PlayBgm(BGMType bgmType, bool once = false)
    {
        bgmAudioSource.clip = bgms[bgmType];
        bgmAudioSource.Play();
        if (once)
        {
            Invoke(nameof(PlayNormalBgm), bgmAudioSource.clip.length);
        }
    }

    private void PlayNormalBgm() => PlayBgm(BGMType.Normal);

    public void PlayAudio(AudioType audioType)
    {
        audioSources[audioType].Play();
    }

    private void PlayJumpAudio() => PlayAudio(AudioType.Jump);
}
