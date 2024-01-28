public class AudioManager : Singleton<AudioManager>
{
    public bool muted;

    public void ToggleMute()
    {
        muted = !muted;
        EventSystem.Instance.Invoke<bool>(EEvent.OnToggleMute, muted);
    }
}