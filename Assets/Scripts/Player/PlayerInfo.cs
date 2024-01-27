public class PlayerInfo : Singleton<PlayerInfo>
{
    public float health;
    public float maxHealth = 10;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
        EventSystem.Instance.AddListener(EEvent.BeforeLoadScene, BeforeLoadScene);

        health = maxHealth;
    }

    private void BeforeLoadScene()
    {
        _instance = null;
        EventSystem.Instance.RemoveListener(EEvent.BeforeLoadScene, BeforeLoadScene);
    }

    public void ChangeHealth(int change)
    {
        health += change;
        EventSystem.Instance.Invoke(EEvent.OnPlayerHealthChange);
    }
}
